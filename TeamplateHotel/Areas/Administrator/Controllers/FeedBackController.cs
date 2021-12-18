using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Database;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class FeedBackController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trang nhận xét khách hàng";
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {

                    List<EFeedBack> list = db.FeedBacks.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).OrderBy(a => a.ID).Select(a => new EFeedBack()
                    {
                        ID = a.ID,
                        Content = a.Content,
                        Image = a.Image,
                        FullName = a.FullName,
                        Investors = a.Investors

                    }).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(EFeedBack model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var insert = new FeedBack()
                        {
                            LanguageID = Request.Cookies["lang_client"].Value,
                            ID = model.ID,
                            FullName = model.FullName,
                            Content = model.Content,
                            Image = model.Image,
                            Investors = model.Investors

                        };

                        db.FeedBacks.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        string message = "Thêm nhận xét thành công";
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
        public JsonResult Edit(EFeedBack model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        FeedBack edit = db.FeedBacks.FirstOrDefault(b => b.ID == model.ID);
                        //string imageSmall = "/Files/_thumbs" + model.Image.Substring(6, model.Image.Length - 6);
                        if (edit != null)
                        {
                            edit.LanguageID = Request.Cookies["lang_client"].Value;

                            edit.ID = model.ID;
                            edit.FullName = model.FullName;
                            edit.Content = model.Content;
                            edit.Image = model.Image;
                            edit.Investors = model.Investors;

                            db.SubmitChanges();

                            string message = "Sửa nhận xét thành công";
                            return Json(new { Result = "OK", Message = message, Record = model });
                        }
                        return Json(new { Result = "ERROR", Message = "Nhận xét không tồn tại" });
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
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    FeedBack del = db.FeedBacks.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.FeedBacks.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = "Error: " + ex.Message });
            }
        }



    }
}
