using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Administrator/Order/

        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang xem yêu cầu đặt hàng";
            return View();
        }

        [HttpPost]
        public JsonResult List(string name = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var db = new MyDbDataContext();
                List<Order> listOrders = db.Orders.OrderByDescending(a => a.ID).ToList();

                var records = listOrders.Select(a => new
                {
                    a.ID,
                    a.Code,
                    CreateDate = a.DateBook.Value.ToShortDateString(),
                    a.LastName,
                    a.Address,
                    //a.Request,
                    a.Email,
                    a.Phone,
                }).Where(x => x.LastName.ToLower().Contains(name.ToLower())).Skip(jtStartIndex).Take(jtPageSize).ToList();
                //Return result to jTable
                return Json(new { Result = "OK", Records = records, TotalRecordCount = listOrders.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var db = new MyDbDataContext();
            Order del = db.Orders.FirstOrDefault(a => a.ID == id);
            if (del == null)
            {
                return Json(new { Result = "ERROR", Message = "Yêu cầu không tồn tại" });
            }
            db.Orders.DeleteOnSubmit(del);
            db.SubmitChanges();
            return Json(new { Result = "OK", Message = "Xóa yêu cầu thành công" });
        }

        [HttpGet]
        public ActionResult Detail(int Id)
        {
            using (var db = new MyDbDataContext())
            {
                Order detailOrder = db.Orders.FirstOrDefault(a => a.ID == Id);
                //if (detailOrder != null)
                //{
                //    if (!detailOrder.Seen.Value)
                //    {
                //        detailOrder.Seen = true;
                //        db.SubmitChanges();
                //    }

                //    return View("Detail", detailOrder);
                //}
                //else
                //{
                //    return Json(new {Result = "ERROR", Message = "Yêu cầu không tồn tại."});

                //}
                if (detailOrder != null)
                {
                    ViewBag.Product = detailOrder.Jacket == true ? "Jacket"
                        : detailOrder.Vest == true ? "Vest"
                        : detailOrder.Suit == true ? "Suit"
                        : detailOrder.Shirt == true ? "Shirt"
                        : detailOrder.Pants == true ? "Pants"
                        : detailOrder.Coat == true ? "Coat"
                        : detailOrder.AllProduct == true ? "All" : "";
                    return View("Detail", detailOrder);
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Yêu cầu không tồn tại." });
                }
            }

        }

    }
}
