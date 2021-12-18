using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class RegisterController : BaseController
    {
        //
        // GET: /Administrator/Register/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang Đăng kí";
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var db = new MyDbDataContext();
                List<Register> listRegister = db.Registers.ToList();

                var records = listRegister.Select(a => new
                {
                    a.ID,
                    CreateDate = a.CreateDate.ToShortDateString(),
                    a.FullName,
                    a.Phone,
                    a.Email,
                }).Skip(jtStartIndex).Take(jtPageSize).ToList();
                //Return result to jTable
                return Json(new {Result = "OK", Records = records, TotalRecordCount = listRegister.Count()});
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var db = new MyDbDataContext();
            Register del = db.Registers.FirstOrDefault(a => a.ID == id);
            if (del == null)
            {
                return Json(new {Result = "ERROR", Message = "Đăng kí không tồn tại"});
            }
            db.Registers.DeleteOnSubmit(del);
            db.SubmitChanges();
            return Json(new {Result = "OK", Message = "Xóa Đăng kí thành công"});
        }

        [HttpGet]
        public ActionResult Detail(int Id)
        {
            var db = new MyDbDataContext();
            Register detail = db.Registers.FirstOrDefault(a => a.ID == Id);
            if (detail == null)
            {
                TempData["Messages"] = "Đăng kí không tồn tại";
                return RedirectToAction("Index");
            }
            return View("Detail", detail);
        }
    }
}