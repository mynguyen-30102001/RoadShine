using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Areas.Administrator.EntityModel;
using TeamplateHotel.Areas.Administrator.Models;
using TeamplateHotel.Areas.Administrator.ModelShow;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class BannerController : BaseController
    {
        //
        // GET: /Administrator/Banner/

        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang banner";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Banner> records =
                    db.Banners.Where(r => r.LanguageID == Request.Cookies["lang_client"].Value).ToList();
                foreach (Banner record in records)
                {
                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(itemAdv, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp banner thành công";
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
                    List<Banner> listSlider =
                        db.Banners.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).ToList();
                    List<ShowSlider> records = listSlider.Select(a => new ShowSlider
                    {
                        ID = a.ID,
                        Title = a.BannerName,
                        Image = a.Image,
                        Link = a.Link,
                        Description = a.Description,
                        Home = a.Home,
                        Status = a.Status,
                        Index = a.Index,
               
                    }).OrderBy(c => c.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = listSlider.Count });
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
            ViewBag.Title = "Thêm mới banner";
             var model = new Banner();

             return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Banner model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    { 
                        var slider = new Banner()
                        {
                            LanguageID = Request.Cookies["lang_client"].Value,
                            BannerName = model.BannerName,
                            Description = model.Description,
                            Home = model.Home,
                            Index = 0,
                            Hot = false,
                            Image = model.Image,
                            Link = model.Link,
                            Status = model.Status
                        };

                        db.Banners.InsertOnSubmit(slider);
                        db.SubmitChanges();

                        TempData["Messages"] = "Thêm mới banner thành công";
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
                Banner detailSlider = db.Banners.FirstOrDefault(a => a.ID == id);
                if (detailSlider == null)
                {
                    TempData["Messages"] = "Banner không tồn tại";
                    return RedirectToAction("Index");
                }
                var banner = new Banner()
                {
                   ID = detailSlider.ID,
                   BannerName = detailSlider.BannerName,
                   Description = detailSlider.Description,
                   Home = detailSlider.Home,
                   Link = detailSlider.Link,
                   Image = detailSlider.Image,
                   //Index = detailSlider.Index,
                   Status = detailSlider.Status,
                };
                ViewBag.Title = "Sửa banner";
                return View(banner);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Banner model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new MyDbDataContext())
                    {
                        Banner slider = db.Banners.FirstOrDefault(b => b.ID == model.ID);
                    
                        if (slider != null)
                        {
                            slider.BannerName = model.BannerName;
                            slider.Description = model.Description;
                            slider.Home = model.Home;
                            slider.Image = model.Image;
                            slider.Link = model.Link;
                            slider.Status = model.Status;


                            slider.LanguageID = Request.Cookies["lang_client"].Value;
                            db.SubmitChanges();
                            TempData["Messages"] = "Sửa slide thành công.";
                            return RedirectToAction("Index");
                        }
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

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Banner del = db.Banners.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.Banners.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa banner thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Banner không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }

        
    }

}
 
