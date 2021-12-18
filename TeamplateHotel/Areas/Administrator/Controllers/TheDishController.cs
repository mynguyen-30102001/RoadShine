//using ProjectLibrary.Database;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using TeamplateHotel.Areas.Administrator.EntityModel;
//using TeamplateHotel.Areas.Administrator.ModelShow;

//namespace TeamplateHotel.Areas.Administrator.Controllers
//{
//    public class TheDishController : Controller
//    {
//        //
//        // GET: /Administrator/TheDish/

//        public ActionResult Index()
//        {
//            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
//            ViewBag.Title = "Thực Đơn Món Ăn";
//            LoadTypeMenu();
//            return View();
//        }

//        [HttpPost]
//        public ActionResult UpdateIndex()
//        {
//            using (var db = new MyDbDataContext())
//            {
//                var records =
//                    db.TheDishes.Join(db.TypeMenus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
//                        a => a.Type,
//                        b => b.Id, (a, b) => new { a }).ToList();
//                foreach (var record in records)
//                {
//                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.a.Type)];
//                    int index;
//                    int.TryParse(itemAdv, out index);
//                    record.a.Index = index;
//                    db.SubmitChanges();
//                }
//                TempData["Messages"] = "Sắp xếp thành công";
//                return RedirectToAction("Index");
//            }
//        }
//        [HttpPost]
//        public JsonResult List(int menuId = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
//        {
//            try
//            {
//                using (var db = new MyDbDataContext())
//                {
//                    List<TheDish> thedish = new List<TheDish>();
//                    if (menuId == 0)
//                    {
//                        thedish = db.TheDishes.ToList();
//                    }
//                    else
//                    {
//                        thedish = db.TheDishes.Where(a => a.Type == menuId).ToList();
//                    }
//                    var listArticle =
//                        thedish.Join(db.TypeMenus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
//                            a => a.Type, b => b.Id, (a, b) => new { a, b });
//                    List<ETheMenu> records = listArticle.Select(a => new ETheMenu
//                    {
//                       Id = a.a.Id,
//                       NameMenu = a.a.NameMenu,
//                       Price = a.a.Price,
//                       PromotionPrice = (double)a.a.PromotionPrice,
//                       Description = a.a.Description,
//                       ShowHome = a.a.ShowHome,
//                       Status = a.a.Status,
//                       NameTypeMenu = a.b.NameTypeMenu,
//                       menuId = a.b.Id,
                       
//                    }).OrderBy(a => a.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
//                    //Return result to jTable
//                    return Json(new { Result = "OK", Records = records, TotalRecordCount = listArticle.Count() });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = "ERROR", ex.Message });
//            }
//        //}
//        //try
//        //{
//        //    using (var db = new MyDbDataContext())
//        //    {
//        //        var listAdv = db.TheDishes.Join(db.TypeMenus, a => a.Type, b => b.Id, (a, b) => new { a, b }).Where(x => x.b.LanguageID == Request.Cookies["lang_client"].Value).Skip(jtStartIndex).Take(jtPageSize).ToList();

//        //        //Get list mới từ list cũ để select items

//        //        //Return result to jTable
//        //        return Json(new { Result = "OK", Records = listAdv.Select(t => new { t.a.Id, t.a.NameMenu, t.a.Description, t.a.Price, t.a.ShowHome, t.a.Status, t.b.NameTypeMenu, t.a.PromotionPrice }), TotalRecordCount = listAdv.Count }, JsonRequestBehavior.AllowGet);
//        //    }
//        //}
//        //catch (Exception ex)
//        //{
//        //    return Json(new { Result = "ERROR", ex.Message });
//        //}
//    }



//        [HttpGet]
//        public ActionResult Create()
//        {
//            LoadData();
//            ViewBag.Title = "Thêm món ăn";
//            return View();
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult Create(ETheMenu model)
//        {
           
//                using (var db = new MyDbDataContext())
//                {
//                    try
//                    {
//                        var insert = new TheDish
//                        {
//                            Id = model.Id,
//                            Alias = model.Alias,
//                            NameMenu = model.NameMenu,
//                            Description = model.Description,
//                            Price = model.Price,
//                            PromotionPrice = model.PromotionPrice,
//                            Image = model.Image,
//                            Index = 0,
//                            MetaDescription = model.MetaDescription,
//                            MetaTitle = model.MetaTitle,
//                            Type = model.Type,
//                            ShowHome = model.ShowHome,
//                            Status = model.Status,
//                        };

//                        db.TheDishes.InsertOnSubmit(insert);
//                        db.SubmitChanges();

//                        TempData["Messages"] = "Thêm món ăn thành công";
//                        return RedirectToAction("Index");
//                    }
//                    catch (Exception exception)
//                    {
//                        ViewBag.Messages = "Error: " + exception.Message;
//                        return View();
//                    }

//                             }
//            return
//                Json(
//                    new
//                    {
//                        Result = " Error",
//                        Errors = ModelState.Errors(),
//                        Message = "Dữ liệu đầu vào không đúng định dang"
//                    }, JsonRequestBehavior.AllowGet);
//        }


//        [HttpGet]
//        public ActionResult Update(int id)
//        {
//            ViewBag.Title = "Cập nhật món ăn";
//            using (var db = new MyDbDataContext())
//            {
//                TheDish detailDish = db.TheDishes.FirstOrDefault(a => a.Id == id);

//                if (detailDish == null)
//                {
//                    TempData["Messages"] = "Món ăn không tồn tại";
//                    return RedirectToAction("Index");
//                }

//                var dish = new ETheMenu
//                {
//                    Id = detailDish.Id,
//                    Alias = detailDish.Alias,
//                    Description = detailDish.Description,
//                    MetaTitle = detailDish.MetaTitle,
//                    MetaDescription = detailDish.MetaDescription,
//                    Image = detailDish.Image,
//                    PromotionPrice = (double)detailDish.PromotionPrice,
//                    Price = detailDish.Price,
//                    Type = detailDish.Type,
//                    NameMenu = detailDish.NameMenu,
//                    ShowHome = detailDish.ShowHome,
//                    Status = detailDish.Status,
//                };
//                LoadData();
//                return View(dish);
//            }
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult Update(ETheMenu model)
//        {
//            using (var db = new MyDbDataContext())
//            {
//                if (ModelState.IsValid)
//                {

//                    try
//                    {
//                        TheDish edit = db.TheDishes.FirstOrDefault(b => b.Id == model.Id);
//                        //string imageSmall = "/Files/_thumbs" + model.SmallImage.Substring(6, model.SmallImage.Length - 6);
//                        if (edit != null)
//                        {
//                            edit.Id = model.Id;
//                            edit.Type = model.Type;
//                            edit.Alias = model.Alias;
//                            edit.MetaTitle = model.MetaTitle;
//                            edit.MetaDescription = model.MetaDescription;
//                            edit.PromotionPrice = model.PromotionPrice;
//                            edit.Image = model.Image;
//                            edit.Description = model.Description;
//                            edit.NameMenu = model.NameMenu;

//                            edit.Price = model.Price;
//                            edit.ShowHome = model.ShowHome;
//                            edit.Status = model.Status;
//                            db.SubmitChanges();

//                            TempData["Messages"] = "Cập nhật món ăn thành công";
//                            return RedirectToAction("Index");
//                        }
//                        return Json(new { Result = "ERROR", Message = "Thực đơn không tồn tại" });
//                    }
//                    catch (Exception exception)
//                    {
//                        ViewBag.Messages = "Lỗi: " + exception.Message;
//                        return View();
//                    }
//                }
//                return
//                    Json(
//                        new
//                        {
//                            Result = " Error",
//                            Errors = ModelState.Errors(),
//                            Message = "Dữ liệu đầu vào không đúng định dạng"
//                        }, JsonRequestBehavior.AllowGet);
//            }
//        }

//        [HttpPost]
//        public JsonResult Delete(int? Id)
//        {
//            try
//            {
//                using (var db = new MyDbDataContext())
//                {
//                    TheDish del = db.TheDishes.FirstOrDefault(c => c.Id == Id);
//                    if (del != null)
//                    {
//                        db.TheDishes.DeleteOnSubmit(del);
//                        db.SubmitChanges();
//                        return Json(new { Result = "OK", Message = "Xóa món ăn thành công" });
//                    }
//                    return Json(new { Result = "ERROR", Message = "Món ăn không tồn tại" });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = "Error", Message = "Error: " + ex.Message });
//            }
//        }
//        //public JsonResult ListMenuByAjax()
//        //{
//        //    using (var db = new MyDbDataContext())
//        //    {
//        //        var listtype = db.TypeMenus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).Select(t => new { Value = t.Id, DisplayText = t.NameTypeMenu }).ToList();
//        //        return Json(new { Result = "OK", Options = listtype });
//        //    }
//        //}
//        //lấy danh sách menu theo ngôn ngữ, theo hotel, theo vị trí, theo AllHotel
      

//        public void LoadData()
//        {
//            using (var db = new MyDbDataContext())
//            {
//                List<SelectListItem> listType = new List<SelectListItem>();
//                listType.Add(new SelectListItem() { Value = "--Lựa chọn nhóm thực đơn --", Text = "0" });
//                var respones = db.TypeMenus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).ToList();

//                foreach (var list in respones)
//                {
//                    listType.Add(new SelectListItem() { Value = list.NameTypeMenu, Text = list.Id.ToString() });
//                }

//                ViewBag.ListTypeId = new SelectList(listType, "Text", "Value");

//            }
//        }
//        public void LoadTypeMenu()
//        {
//            using (var db = new MyDbDataContext())
//            {
//                List<SelectListItem> listMenu = new List<SelectListItem>();
//                listMenu.Add(new SelectListItem() { Value = "0", Text = "--Lựa chọn nhóm thực đơn --" });
//                var respones = db.TypeMenus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).ToList();

//                foreach (var list in respones)
//                {
//                    listMenu.Add(new SelectListItem() { Value = list.Id.ToString(), Text =  list.NameTypeMenu });
//                }

//                ViewBag.ListMenu = listMenu;
//            }
//        }
//    }

//}

