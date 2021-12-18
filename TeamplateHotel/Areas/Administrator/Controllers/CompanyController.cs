using System;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;
using ProjectLibrary.Security;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class CompanyController : BaseController
    {
        // GET: /Administrator/Article/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang Công ty";
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var list = db.Companies.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).Select(a => new
                    {
                        a.ID,
                        a.Name,
                        a.Logo,
                        a.Image,
                        a.Email,
                        a.Tel,
                        a.Address
                    }).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = list, TotalRecordCount = list.Count()});
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
            using (var db = new MyDbDataContext())
            {
                if (db.Companies.Any(a => a.LanguageID == Request.Cookies["lang_client"].Value))
                {
                    TempData["Messages"] = "Website đã tồn tại khách sạn rồi";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Title = "Thêm Khách sạn";
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ECompany model)
        {
            using (var db = new MyDbDataContext())
            {
                if (db.Companies.Any(a => a.LanguageID == Request.Cookies["lang_client"].Value))
                {
                    TempData["Messages"] = "Website đã tồn tại khách sạn rồi";
                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        var hotel = new Company()
                        {
                            LanguageID = Request.Cookies["lang_client"].Value,
                            Hotline = model.Hotline,
                            Name = model.Name,
                            Logo = model.Logo,
                            Image = model.Image,
                            Tel = model.Tel,
                            Fax = model.Fax,
                            Email = model.Email,
                            Address = model.Address,
                            Location = model.Location,
                            CodeBooking = model.CodeBooking,
                            Website = model.Website,
                            Condition = model.Condition,
                            Tripadvisor = model.Tripadvisor,
                            Facebook = model.FaceBook,
                            Instagram = model.Instagram,
                            Twitter = model.Twitter,
                            Youtube = model.Youtube,
                            CopyRight = model.CopyRight,
                            MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Name : model.MetaTitle,
                            MetaDescription =
                                string.IsNullOrEmpty(model.MetaDescription) ? model.Name : model.MetaDescription
                        };

                        db.Companies.InsertOnSubmit(hotel);
                        db.SubmitChanges();

                        TempData["Messages"] = "Thêm khách sạn thành công";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Lỗi: " + exception.Message;
                        return View();
                    }
                }
                return View();
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.Title = "Cập nhật khách sạn";
            using (var db = new MyDbDataContext())
            {
                Company hotel = db.Companies.FirstOrDefault(a => a.ID == id);

                if (hotel == null)
                {
                    TempData["Messages"] = "Khách sạn không tồn tại";
                    return RedirectToAction("Index");
                }

                var eHotel = new ECompany
                {
                    Name = hotel.Name,
                    Logo = hotel.Logo,
                    Hotline = hotel.Hotline,
                    Image = hotel.Image,
                    Tel = hotel.Tel,
                    Fax = hotel.Fax,
                    Email = hotel.Email,
                    Address = hotel.Address,
                    Location = hotel.Location,
                    CodeBooking = hotel.CodeBooking,
                    Website = hotel.Website,
                    Condition = hotel.Condition,
                    Tripadvisor = hotel.Tripadvisor,
                    FaceBook = hotel.Facebook,
                    Instagram = hotel.Instagram,
                    Twitter = hotel.Twitter,
                    Youtube = hotel.Youtube,
                    CopyRight = hotel.CopyRight,
                    MetaTitle = hotel.MetaTitle,
                    MetaDescription = hotel.MetaDescription
                };
                return View(eHotel);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ECompany model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Company hotel = db.Companies.FirstOrDefault(b => b.ID == model.ID);
                        if (hotel != null)
                        {
                            hotel.Name = model.Name;
                            hotel.Logo = model.Logo;
                            hotel.Image = model.Image;
                            hotel.Tel = model.Tel;
                            hotel.Hotline = model.Hotline;
                            hotel.Fax = model.Fax;
                            hotel.Email = model.Email;
                            hotel.Address = model.Address;
                            hotel.Location = model.Location;
                            hotel.CodeBooking = model.CodeBooking;
                            hotel.Website = model.Website;
                            hotel.Condition = model.Condition;
                            hotel.Tripadvisor = model.Tripadvisor;
                            hotel.Facebook = model.FaceBook;
                            hotel.Instagram = model.Instagram;
                            hotel.Twitter = model.Twitter;
                            hotel.Youtube = model.Youtube;
                            hotel.CopyRight = model.CopyRight;
                            hotel.MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Name : model.MetaTitle;
                            hotel.MetaDescription = string.IsNullOrEmpty(model.MetaDescription)
                                ? model.Name
                                : model.MetaDescription;

                            db.SubmitChanges();
                            TempData["Messages"] = "Cập nhật khách sạn thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Lỗi: " + exception.Message;
                        return View();
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
                    Company del = db.Companies.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.Companies.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa khách sạn thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "Khách sạn không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }
    }
}