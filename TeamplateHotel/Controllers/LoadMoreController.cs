using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Database;

namespace TeamplateHotel.Controllers
{
    public class LoadMoreController : BasicController
    {
        //
        // GET: /LoadMore/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public  JsonResult GetData(int pageIndex, int pageSize)
        {
            using (var db = new MyDbDataContext())
            {
                
                var query = db.Products.Where(a => a.Status);

                var value = query.Skip(pageIndex * pageSize).Take(pageSize).Select(x=>new{x.ProductName}).ToList();

                return Json(new { data = value, status = true }, JsonRequestBehavior.AllowGet);
            }
            //var db = new MyDbDataContext();
            //var query = db.Products.Where(a => a.Status).OrderBy(a => a.ProductName).Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);

        }
    }
}
