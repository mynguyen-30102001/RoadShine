using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Administrator/Project/


        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Danh sách Dự án ";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {

                List<Poject> records = db.Pojects.ToList();

                foreach (Poject record in records)
                {
                    string item = Request.Params[string.Format("Sort[{0}].Index", record.PojectID)];
                    int index;
                    int.TryParse(item, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }

                TempData["Messages"] = "Sắp xếp thành công";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var list =
                        db.Pojects.Join(db.Menus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
                            a => a.MenuID, b => b.ID, (a, b) => new
                            {
                                a.PojectID,
                                a.Title,
                                a.Index,
                                a.Status,
                                a.ShowHome
                            }).OrderBy(a => a.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm Dự án ";
            var eProject = new EProject();
            LoadData();
            return View(eProject);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EProject model)
        {
            using (var db = new MyDbDataContext())
            {

                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = StringHelper.ConvertToAlias(model.Title);
                }

                try
                {
                    var poject = new Poject
                    {
                        MenuID = model.MenuID,
                        Title = model.Title,
                        Alias = model.Alias,
                        Image = model.Image,
                        Index = 0,
                        Description = model.Description,
                        MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle,
                        MetaDescription =
                            string.IsNullOrEmpty(model.MetaDescription) ? model.Title : model.MetaDescription,
                        Status = model.Status,
                        ShowHome = model.ShowHome
                    };

                    db.Pojects.InsertOnSubmit(poject);
                    db.SubmitChanges();

                    //Thêm hình ảnh cho dich vu
                    if (model.EGalleryITems != null)
                    {
                        foreach (EGalleryITem itemGallery in model.EGalleryITems)
                        {
                            var pojectGallery = new PojectGallery
                            {
                                ImageLarge = itemGallery.Image,
                                //ImageSmall = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                PojectID = poject.PojectID,
                            };
                            db.PojectGalleries.InsertOnSubmit(pojectGallery);
                        }

                        db.SubmitChanges();
                    }


                    TempData["Messages"] = "Thêm Dự án thành công.";
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ViewBag.Messages = "Error: " + exception.Message;
                    LoadData();
                    return View(model);
                } 
                LoadData();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Poject service = db.Pojects.FirstOrDefault(a => a.PojectID == id);
                if (service == null)
                {
                    TempData["Messages"] = "Dự án không tồn tại";
                    return RedirectToAction("Index");
                }
                ViewBag.Title = "Sửa dự án";
                var eService = new EProject
                {
                    PojectID = service.PojectID,
                    MenuID = service.MenuID,
                    Title = service.Title,
                    Alias = service.Alias,
                    Image = service.Image,
                    Index = service.Index,
                    Description = service.Description,
                     MetaTitle = service.MetaTitle,
                    MetaDescription = service.MetaDescription,
                    Status = service.Status,
                    ShowHome  = service.ShowHome
                };
                //lấy danh sách hình ảnh
                eService.EGalleryITems =
                    db.PojectGalleries.Where(a => a.PojectID == service.PojectID).Select(a => new EGalleryITem
                    {
                        Image = a.ImageLarge
                    }).ToList();
                LoadData();
                return View(eService);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(EProject model)
        {
            using (var db = new MyDbDataContext())
            {
                 
                    try
                    {
                        Poject service = db.Pojects.FirstOrDefault(b => b.PojectID == model.PojectID);
                        if (service != null)
                        {
                            service.Title = model.Title;
                            service.MenuID = model.MenuID;
                            service.Alias = model.Alias;
                            service.Image = model.Image;
                            service.Description = model.Description;
                            service.MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle;
                            service.MetaDescription = string.IsNullOrEmpty(model.MetaDescription)
                                ? model.Title
                                : model.MetaDescription;
                            service.Status = model.Status;
                            service.ShowHome = model.ShowHome;

                            db.SubmitChanges();

                            //xóa gallery cho phòng
                            db.PojectGalleries.DeleteAllOnSubmit(
                                db.PojectGalleries.Where(a => a.PojectID == service.PojectID).ToList());
                            //Thêm hình ảnh cho phòng
                            if (model.EGalleryITems != null)
                            {
                                foreach (EGalleryITem itemGallery in model.EGalleryITems)
                                {
                                    var serviceGallery = new PojectGallery
                                    {
                                        ImageLarge = itemGallery.Image,
                                        //ImageSmall = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                        PojectID = service.PojectID,
                                    };
                                    db.PojectGalleries.InsertOnSubmit(serviceGallery);
                                }
                                db.SubmitChanges();
                            }
                            TempData["Messages"] = "Sửa Dự án thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Error: " + exception.Message;
                        LoadData();
                        return View(model);
                    }
               
                LoadData();
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int PojectID)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Poject del = db.Pojects.FirstOrDefault(c => c.PojectID == PojectID);
                    if (del != null)
                    {
                        //xóa hết hình ảnh của phòng này
                        db.PojectGalleries.DeleteAllOnSubmit(
                            db.PojectGalleries.Where(a => a.PojectID == del.PojectID).ToList());

                        db.Pojects.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa Dự án thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Dự án không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }

        public void LoadData()
        {
            var listMenu = new List<SelectListItem>
            {
                new SelectListItem {Value = "0", Text = "Lựa chọn chuyên mục"}
            };
            var menus = new List<Menu>();

            //menus =
            //    MenuController.GetListMenu(0, Request.Cookies["lang_client"].Value).Where(
            //        a =>
            //            a.Type == SystemMenuType.Project).ToList();

            foreach (Menu menu in menus)
            {
                string sub = "";
                for (int i = 0; i < menu.Level; i++)
                {
                    sub += "|--";
                }

                menu.Title = sub + menu.Title;
            }

            listMenu.AddRange(menus.OrderBy(a => a.Location).Select(a => new SelectListItem
            {
                Text = a.Title,
                Value = a.ID.ToString()
            }).ToList());
            ViewBag.ListMenu = listMenu;
        }
    }

}
