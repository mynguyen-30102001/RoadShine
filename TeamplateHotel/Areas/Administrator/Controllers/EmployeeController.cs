using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;





/////////////////
namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class EmployeeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trang nhân viên tư vấn";
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
                   
                    List<Employee> listem = db.Employees.ToList();

                    var list = listem.Select(a => new
                    {
                        a.Id,
                        a.FullName,
                        a.Job,
                        a.Image,
                        a.Facebook,
                        a.Skype,
                        a.Phone,
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
        public JsonResult Create(EEmployee model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var insert = new Employee
                        {
                            Id = model.Id,
                            FullName = model.FullName,
                            Job = model.Job,
                            Image = model.Image,
                            Facebook = model.Facebook,
                            Skype = model.Skype,
                            Phone = model.Phone,

                        };

                        db.Employees.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        string message = "Thêm nhân viên thành công";
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
        public JsonResult Edit(Employee model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Employee edit = db.Employees.FirstOrDefault(b => b.Id == model.Id);
                        //string imageSmall = "/Files/_thumbs" + model.Image.Substring(6, model.Image.Length - 6);
                        if (edit != null)
                        {
                            edit.Id = model.Id;
                            edit.FullName = model.FullName;
                            edit.Job = model.Job;
                            edit.Image = model.Image;
                            edit.Facebook = model.Facebook;
                            edit.Skype = model.Skype;
                            edit.Phone = model.Phone;
                           

                            db.SubmitChanges();

                            string message = "Sửa nhân viên thành công";
                            return Json(new { Result = "OK", Message = message, Record = model });
                        }
                        return Json(new { Result = "ERROR", Message = "nhân viên không tồn tại" });
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
                    Employee del = db.Employees.FirstOrDefault(c => c.Id == id);
                    if (del != null)
                    {
                        db.Employees.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa nhân viên thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "nhân viên không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = "Error: " + ex.Message });
            }
        }

     
    }
}