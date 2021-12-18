using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class GalleryController : BaseController
    {
        //
        // GET: /Administrator/Gallery/

        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang Gallery";
            return View();

        }
        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Gallery> records = db.Galleries.ToList();
                foreach (Gallery record in records)
                {
                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.GalleryId)];
                    int index;
                    int.TryParse(itemAdv, out index);
                    record.IndexGallery = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp gallary thành công";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var records =
                            db.Galleries.ToList();
                        foreach (var record in records)
                        {
                            string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.GalleryId)];
                            int index;
                            int.TryParse(itemAdv, out index);
                            record.IndexGallery = index;
                            db.SubmitChanges();
                        }
                        TempData["Messages"] = "Sắp xếp thành công";
                        return Json(new { Result = "OK", Records = records, TotalRecordCount = records.Count }, JsonRequestBehavior.AllowGet);
                    }

                    catch (Exception ex)
                    {
                        return Json(new { Result = "ERROR", message = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(
                        new
                        {
                            Result = " Error",
                            Errors = ModelState.Errors(),
                            Message = "Không có giữ liệu để hiển thị."
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(EGallery model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var insert = new Gallery
                        {
                            GalleryId = model.GalleryId,
                            TitleGallery = model.TitleGallery,
                            IndexGallery = 0,
                            ImageThump = model.ImageThump,
                            ImageLarge = model.ImageLarge,
                         };

                        db.Galleries.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        string message = "Thêm gallery thành công";
                        return Json(new { Result = "OK", Message = message, Record = model });
                    }
                    catch (Exception exception)
                    {
                        return Json(new { Result = "Error", Message = "Error: " + exception.Message });
                    }
                }
                return
                    Json(
                        new
                        {
                            Result = " Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dang"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(EGallery model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        Gallery edit = db.Galleries.FirstOrDefault(b => b.GalleryId == model.GalleryId);
                        //string imageSmall = "/Files/_thumbs" + model.SmallImage.Substring(6, model.SmallImage.Length - 6);
                        if (edit != null)
                        {
                            edit.GalleryId = model.GalleryId;
                            edit.TitleGallery = model.TitleGallery;
                            edit.ImageLarge = model.ImageLarge;
                            edit.ImageThump = model.ImageThump;
                             db.SubmitChanges();

                            string message = "Sửa gallery thành công";
                            return Json(new { Result = "OK", Message = message, Record = model });
                        }
                        return Json(new { Result = "ERROR", Message = "Gallery không tồn tại" });
                    }
                    catch (Exception exception)
                    {
                        return Json(new { Result = "Error", Message = "Error: " + exception.Message });
                    }
                }
                return
                    Json(
                        new
                        {
                            Result = " Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dạng"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(int? GalleryId)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Gallery del = db.Galleries.FirstOrDefault(c => c.GalleryId == GalleryId);
                    if (del != null)
                    {
                        db.Galleries.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa gallery thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Gallery không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = "Error: " + ex.Message });
            }
        }
         
        //    public void LoadData()
        //    {
        //        var listMenu = new List<SelectListItem>
        //        {
        //            new SelectListItem {Value = "0", Text = "Lựa chọn chuyên mục"}
        //        };
        //        var menus = new List<Menu>();

        //        menus =
        //            MenuController.GetListMenu(0, Request.Cookies["lang_client"].Value).Where(
        //                a =>
        //                    a.Type == SystemMenuType.Gallery).ToList();

        //        foreach (Menu menu in menus)
        //        {
        //            string sub = "";
        //            for (int i = 0; i < menu.Level; i++)
        //            {
        //                sub += "|--";
        //            }
        //            menu.Title = sub + menu.Title;
        //        }

        //        listMenu.AddRange(menus.OrderBy(a => a.Location).Select(a => new SelectListItem
        //        {
        //            Text = a.Title,
        //            Value = a.ID.ToString()
        //        }).ToList());
        //        ViewBag.ListMenu = listMenu;
        //    }
        //}
    }
}