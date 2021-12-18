using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ReservationController : BaseController
    {
        //
        // GET: /Administrator/Reservation/

        public ActionResult Index()
        {

            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang xem yêu cầu đặt bàn";
            ViewBag.ReservationsOnline = true;
            return View();
        }
        [HttpPost]
        public JsonResult List(string name = "", string checkin = "", string checkout = "", string bookdate = "", string timebook = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<Reservation> listrReservations = db.Reservations.OrderByDescending(a => a.ReservationId).ToList();


                    if (string.IsNullOrEmpty(bookdate) == false)
                    {
                        DateTime book_date = Convert.ToDateTime(bookdate);
                        listrReservations =
                            listrReservations.Where(a => a.CreateDate.Date >= book_date.Date).OrderBy(a => a.CreateDate).ToList();
                    }
                    var records = listrReservations.Select(a => new
                    {
                        a.ReservationId,
                        //a.CreateDate,
                        a.MessageB,
                        //a._Time,
                        a.Party,
                        a.Name,
                        a.Email,
                        a.Tel,
                        a.Seen,
                        a.NumberPeople,

                        checkin = a.CheckIn.ToString("dd,MMMM,yyyy"),
                        timebook =  a.TimeB.ToString()  ,
                        //timebook = a._Time.ToString(@"hh:mm:ss tt", new CultureInfo("en-US")),

                        DateBook = a.CreateDate.ToString("dd, MMMM, yyyy"),
                    }).Where(x=>x.Name.ToLower().Contains(name.ToLower())).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = listrReservations.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
        [HttpGet]
        public ActionResult Detail(int Id)
        {
            using (var db = new MyDbDataContext())
            {
                Reservation detailRegisterRoom = db.Reservations.FirstOrDefault(a => a.ReservationId == Id);
                if (detailRegisterRoom != null)
                {
                    if (!detailRegisterRoom.Seen.Value)
                    {
                        detailRegisterRoom.Seen = true;
                        db.SubmitChanges();
                    }
                    return View("Detail", detailRegisterRoom);
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Yêu cầu không tồn tại." });

                }
                //if (detailRegisterRoom == null)
                //{
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    var changeSeen = db.Reservations.FirstOrDefault(a => a.Seen == false);
                //    if (changeSeen !=null)
                //    {
                //        var typeMenu = 
                //        { 
                //            ReservationId = Id,
                //            Seen = true, 

                //        };
                //        db.Reservations.InsertOnSubmit(typeMenu);
                //        db.SubmitChanges();
                //        return View("Detail", detailRegisterRoom);

                //    }
                //    return RedirectToAction("Index");

                //}
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult Delete(int? ReservationId)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Reservation del = db.Reservations.FirstOrDefault(c => c.ReservationId == ReservationId);
                    if (del != null)
                    {
                        db.Reservations.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa yêu cầu thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Yêu cầu không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = "Error: " + ex.Message });
            }
        }

    }
}
