using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: /Administrator/Room/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang quản ký khách hàng";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Customer> records = db.Customers.Where(r => r.LanguageID == Request.Cookies["lang_client"].Value).ToList();
                foreach (Customer record in records)
                {
                    string itemRoom = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(itemRoom, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp khách  hàng thành công";
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
                    List<Customer> list = db.Customers.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).ToList();
                    var records = list.Select(a => new
                    {
                        a.ID,
                        a.Title,
                        a.Index,
                        a.Status,
                        a.Home
                    }).OrderBy(a => a.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = records, TotalRecordCount = list.Count});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm Khách hàng";
            var eRoom = new ECustomer();
            return View(eRoom);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ECustomer model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Title);
                    }
                    try
                    {
                        var room = new Customer()
                        {
                            LanguageID = Request.Cookies["lang_client"].Value,
                            Title = model.Title,
                            Alias = model.Alias,
                            Image = model.Image,
                            Index = 0,
                            Description = model.Description,
                            Content = model.Content,
                            MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle,
                            MetaDescription =
                                string.IsNullOrEmpty(model.MetaDescription) ? model.Title : model.MetaDescription,
                            Status = model.Status,
                            Home = model.Home
                        };

                        db.Customers.InsertOnSubmit(room);
                        db.SubmitChanges();

                        
                        TempData["Messages"] = "Thêm khách hàng thành công.";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Error: " + exception.Message;
                        return View(model);
                    }
                }
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Customer detailRoom = db.Customers.FirstOrDefault(a => a.ID == id);
                if (detailRoom == null)
                {
                    TempData["Messages"] = "Khách hàng không tồn tại";
                    return RedirectToAction("Index");
                }
                ViewBag.Title = "Sửa khách hàng";
                var room = new ECustomer
                {
                    ID = detailRoom.ID,
                    Title = detailRoom.Title,
                    Alias = detailRoom.Alias,
                    Image = detailRoom.Image,
                    Index = detailRoom.Index,
                    Description = detailRoom.Description,
                    Content = detailRoom.Content,
                    MetaTitle = detailRoom.MetaTitle,
                    MetaDescription = detailRoom.MetaDescription,
                    Status = detailRoom.Status,
                    Home = detailRoom.Home
                };
                //lấy danh sách hình ảnh
               return View(room);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ECustomer model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Customer room = db.Customers.FirstOrDefault(b => b.ID == model.ID);
                        if (room != null)
                        {
                            room.Title = model.Title;
                            room.Alias = model.Alias;
                            room.Image = model.Image;
                            room.Description = model.Description;
                            room.Content = model.Content;
                            room.MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle;
                            room.MetaDescription = string.IsNullOrEmpty(model.MetaDescription)
                                ? model.Title
                                : model.MetaDescription;
                            room.Status = model.Status;
                            room.Home = model.Home;

                            db.SubmitChanges();

                           
                            TempData["Messages"] = "Sửa khách hàng thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Error: " + exception.Message;
                        return View(model);
                    }
                }
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Customer del = db.Customers.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        //xóa hết hình ảnh của phòng này
                        db.Customers.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa khách hàng thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "khách hàng không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }
    }
}