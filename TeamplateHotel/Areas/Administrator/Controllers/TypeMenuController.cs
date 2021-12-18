//using ProjectLibrary.Database;
//using ProjectLibrary.Utility;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using TeamplateHotel.Areas.Administrator.EntityModel;
//using TeamplateHotel.Areas.Administrator.Models;

//namespace TeamplateHotel.Areas.Administrator.Controllers
//{
//    public class TypeMenuController : BaseController
//    {
//        //
//        // GET: /Administrator/TypeMenu/

//        public ActionResult Index()
//        {
//            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
//            ViewBag.Title = "Nhóm Thực Đơn";
//            return View();
//        }

//        [HttpPost]
//        public ActionResult UpdateIndex()
//        {
//            using (var db = new MyDbDataContext())
//            {
//                var records =
//                    db.TypeMenus.ToList();
//                foreach (var record in records)
//                {
//                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.Id)];
//                    int index;
//                    int.TryParse(itemAdv, out index);
//                    record.Index = index;
//                    db.SubmitChanges();
//                }
//                TempData["Messages"] = "Sắp xếp thành công";
//                return RedirectToAction("Index");
//            }
//        }

//        [HttpPost]
//        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
//        {
//            try
//            {
//                using (var db = new MyDbDataContext())
//                {
//                    List<TypeMenu> listAdv = db.TypeMenus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).ToList();
//                    //Return result to jTable
//                    return Json(new { Result = "OK", Records = listAdv.Select(t => new { t.Id, t.NameTypeMenu, t.Description, t.Image, t.ShowHome, t.Index, t.Status, t.Alias }), TotalRecordCount = listAdv.Count }, JsonRequestBehavior.AllowGet);
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = "ERROR", ex.Message });
//            }
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public JsonResult Create(ETypeMenu model)
//        {

//            using (var db = new MyDbDataContext())
//            {
//                if (ModelState.IsValid)
//                {
//                    if (string.IsNullOrEmpty(model.Alias))
//                    {
//                        model.Alias = StringHelper.ConvertToAlias(model.NameTypeMenu);
//                    }

//                    TypeMenu checkMenuAlias = db.TypeMenus.FirstOrDefault(a => a.Alias == model.Alias);
//                    if (checkMenuAlias != null)
//                    {
//                        ModelState.AddModelError("Alias", "Alias này đã tồn tại trong hệ thống");
//                        //return Json(new { Result = "OK", Message = "Alias này đã tồn tại trong hệ thống" });

//                    }
//                    else
//                    {
//                        try
//                        {
//                            var insert = new TypeMenu
//                            {
//                                LanguageID = Request.Cookies["lang_client"].Value,
//                                Id = model.Id,
//                                NameTypeMenu = model.NameTypeMenu,
//                                Alias = model.Alias,
//                                Description = model.Description,
//                                Image = model.Image,
//                                Index = 0,
//                                ShowHome = model.ShowHome,
//                                Status = model.Status,
//                            };

//                            db.TypeMenus.InsertOnSubmit(insert);
//                            db.SubmitChanges();
//                            string message = "Thêm nhóm thực đơn thành công";
//                            return Json(new { Result = "OK", Message = message, Record = model });
//                        }
//                        catch (Exception exception)
//                        {
//                            return Json(new { Result = "Error", Message = "Error: " + exception.Message });
//                        }
//                    }

//                }


//                return
//                    Json(
//                        new
//                        {
//                            Result = " Error",
//                            Errors = ModelState.Errors(),
//                            Message = "Alias này đã tồn tại trong hệ thống"
//                        }, JsonRequestBehavior.AllowGet);
//            }
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public JsonResult Update(ETypeMenu model)
//        {
//            using (var db = new MyDbDataContext())
//            {
//                if (ModelState.IsValid)
//                {
//                    //if (string.IsNullOrEmpty(model.Alias))
//                    //{
//                    //    model.Alias = StringHelper.ConvertToAlias(model.NameTypeMenu);
//                    //}

//                    //TypeMenu checkMenuAlias = db.TypeMenus.FirstOrDefault(a => a.Alias == model.Alias);
//                    //if (checkMenuAlias != null)
//                    //{
//                    //    ModelState.AddModelError("Alias", "Menu này đã tồn tại trong hệ thống");
//                    //}
//                    //Kiểm tra xem alias thuộc hotel này đã tồn tại chưa
                     
//                    //TypeMenu checkMenuAlias = db.TypeMenus.FirstOrDefault(m => m.Alias == model.Alias && m.Id != model.Id);
//                    //if (checkMenuAlias == null)
//                    //{
//                    //    ModelState.AddModelError("Alias", "Chuyên mục này đã tồn tại trong hệ thống");
//                    //}
//                    //else
//                    //{
//                        try
//                        {
//                            TypeMenu edit = db.TypeMenus.FirstOrDefault(b => b.Id == model.Id);
//                            //string imageSmall = "/Files/_thumbs" + model.SmallImage.Substring(6, model.SmallImage.Length - 6);
//                            if (edit != null)
//                            {
//                                edit.LanguageID = Request.Cookies["lang_client"].Value;
//                                edit.NameTypeMenu = model.NameTypeMenu;
//                                edit.Id = model.Id;
//                                edit.Alias = StringHelper.ConvertToAlias(model.NameTypeMenu);
//                                //edit.Alias = model.Alias;
//                                edit.Description = model.Description;
//                                edit.Image = model.Image;
//                                edit.Index = model.Index;
//                                edit.ShowHome = model.ShowHome;
//                                edit.Status = model.Status;
//                                db.SubmitChanges();

//                                string message = "Sửa nhóm thực đơn thành công";
//                                return Json(new { Result = "OK", Message = message, Record = model });
//                            }
//                            return Json(new { Result = "ERROR", Message = "Nhóm thực đơn không tồn tại" });
//                        }
//                        catch (Exception exception)
//                        {
//                            return Json(new { Result = "Error", Message = "Error: " + exception.Message });
//                        }

//                    }
//                //}
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
//        public JsonResult Delete(int Id)
//        {
//            try
//            {
//                using (var db = new MyDbDataContext())
//                {
//                    var dish = db.TheDishes.FirstOrDefault(a => a.Type == Id);
//                    var del = db.TypeMenus.FirstOrDefault(c => c.Id == Id);

//                    if (del != null)
//                    {

//                        db.TypeMenus.DeleteOnSubmit(del);
//                        db.SubmitChanges();
//                        return Json(new { Result = "OK", Message = "Xóa nhóm thực đơn thành công" });

//                    }

//                }
//                return Json(new { Result = "ERROR", Message = "Nhóm thực đơn không tồn tại" });


//            }
//            catch (Exception ex)
//            {
//                string message = "Đang có món ăn tồn tại trong menu này!";
//                return Json(new { Result = "OK", Message = message });

//            }


//        }
//    }
//}
