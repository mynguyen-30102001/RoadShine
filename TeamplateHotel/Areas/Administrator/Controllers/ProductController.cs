using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Administrator/Product/
        private readonly MyDbDataContext db = new MyDbDataContext();

        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Danh sách sản phẩm";
            LoadTypeMenu();
            return View();
        }

        //[HttpPost]
        //public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        var list = db.Products.Select(a => new
        //        {
        //            a.ProductID,
        //            a.ProductName,
        //            a.Price,
        //            a.Image,
        //            a.Description,
        //            a.Status,
        //        }).OrderBy(a => a.ProductID).ToList();
        //        return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count() });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", ex.Message });
        //    }

        //}

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                var records =
                    db.Products.Join(db.Menus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
                        a => a.MenuID,
                        b => b.ID, (a, b) => new { a }).ToList();
                foreach (var record in records)
                {
                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.a.ProductID)];
                    int index;
                    int.TryParse(itemAdv, out index);
                    record.a.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public JsonResult List(int menuId = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<Product> thedish = new List<Product>();
                    if (menuId == 0)
                    {
                        thedish = db.Products.ToList();
                    }
                    else
                    {
                        thedish = db.Products.Where(a => a.MenuID == menuId).ToList();
                    }
                    var listArticle =
                        thedish.Join(db.Menus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
                            a => a.MenuID, b => b.ID, (a, b) => new { a, b });
                    List<ETheMenu> records = listArticle.Select(a => new ETheMenu
                    {
                        ProductID = a.a.ProductID,
                        NameMenu = a.a.ProductName,
                        //Price = a.a.Price,
                        //PromotionPrice = (double)a.a.PromotionPrice,
                        Description = a.a.Description,
                         Status = a.a.Status,
                         Image =  a.a.Image,
                         BestSale = a.a.BestSale,
                         Index =  a.a.Index,
                        NameTypeMenu = a.b.Title,
                        menuId = a.b.ID,

                    }).OrderBy(a => a.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = listArticle.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
            //}
            //try
            //{
            //    using (var db = new MyDbDataContext())
            //    {
            //        var listAdv = db.TheDishes.Join(db.TypeMenus, a => a.Type, b => b.Id, (a, b) => new { a, b }).Where(x => x.b.LanguageID == Request.Cookies["lang_client"].Value).Skip(jtStartIndex).Take(jtPageSize).ToList();

            //        //Get list mới từ list cũ để select items

            //        //Return result to jTable
            //        return Json(new { Result = "OK", Records = listAdv.Select(t => new { t.a.Id, t.a.NameMenu, t.a.Description, t.a.Price, t.a.ShowHome, t.a.Status, t.b.NameTypeMenu, t.a.PromotionPrice }), TotalRecordCount = listAdv.Count }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", ex.Message });
            //}
        }


        [HttpGet]
        public ActionResult Create(int id)
        {
            LoadData();
            using (var db = new MyDbDataContext())
            {
                List<SelectListItem> listtype = new List<SelectListItem>();
                //foreach (var b in db.Menus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value))
                //{
                //    if (b.Type == SystemMenuType.ProductNew)
                //    {
                //        listtype.Add(new SelectListItem() { Value = b.Title, Text = b.ID.ToString() });
                //    }

                //}
                ViewBag.ListMenuID = new SelectList(listtype, "Text", "Value");

                if (id == 0)
                {
                    var product = new EProduct();
                    ViewBag.cmd = "Create";
                    ViewBag.Title = "Thêm bài viết";
                    return View(product);
                }

                Product model = db.Products.FirstOrDefault(a => a.ProductID == id);
                if (model == null)
                {
                    return PartialView("Create");
                }

                var eproduct = new EProduct
                {
                    ProductID = model.ProductID,
                    ProductName = model.ProductName,
                    MenuID = model.MenuID,
                    Alias = model.Alias,
                    LanguageID = model.LanguageID,
                    //Price = (float)model.Price,
                    //PromotionPrice = (float)model.PromotionPrice,
                    MetaDescription = model.MetaDescription,
                    MetaTitle = model.MetaTitle,
                    Image = model.Image,
                    //Image2 = model.Image2,
                    //Producer = model.Producer,
                    Technicadata = model.Technicadata,
                    //Description = model.Description,
                    Content = model.Content,
                    Categories = model.Categories,
                    Status = model.Status,
                    //Hot = model.Hot,
                    BestSale = model.BestSale,
                    Index = (int)model.Index,
                    TBR = (bool)model.TBR,
                    PCR = (bool)model.PCR,
                    OTR = (bool)model.OTR,

                };
                eproduct.ProductGalleries = db.ProductGalleries.Where(a => a.ProductID == model.ProductID).ToList();

                if (!string.IsNullOrEmpty(model.TypeMenuID))
                {
                    string[] select = model.TypeMenuID.Split(',');
                    eproduct.TypeMenuID = select;
                }
                //if (!string.IsNullOrEmpty(model2.))
                //{
                //    model.ProductGalleries = db.ProductGalleries.Where(a => a.ProductID == eproduct.ProductID).ToList();

                //}

                return View(eproduct);

            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(FormCollection frm , EProduct model2)
        {
            using (var db = new MyDbDataContext())
            {
                try
                {
                    var model = new Product
                    {
                        LanguageID = Request.Cookies["lang_client"].Value,
                        Alias = frm["Alias"],
                        Image = frm["Image"],
                        //Image2 = frm["Image2"],
                        //TypeMenuID = frm["TypeMenuID"],
                        MenuID = Int32.Parse(frm["MenuID"]),
                        ProductName = frm["ProductName"],
                        //Price = Math.Round(float.Parse(frm["Price"]), 2),
                        //PromotionPrice = Math.Round(float.Parse(frm["PromotionPrice"]), 2),
                        //Description = frm["Description"],
                        //Producer = frm["Producer"],
                        Technicadata = frm["Technicadata"],
                        MetaTitle = frm["MetaTitle"],
                        MetaDescription = frm["MetaDescription"],
                        Content = frm["Content"],
                        Categories = frm["Categories"],
                        //Hot = bool.Parse(frm["Hot"]),
                        BestSale = bool.Parse(frm["BestSale"]),
                        Index = 0,
                        Status = bool.Parse(frm["Status"]),
                        TBR = bool.Parse(frm["TBR"]),
                        PCR = bool.Parse(frm["PCR"]),
                        OTR = bool.Parse(frm["OTR"]),
                    };
                    db.Products.InsertOnSubmit(model);
                    db.SubmitChanges();


                    //Thêm hình ảnh cho  product
                     if (model2.EGalleryITems != null)
                    {
                        foreach (var itemGallery in model2.EGalleryITems)
                        {
                            ProductGallery productGallery = new ProductGallery
                            {
                                ImageLarge = itemGallery.Image,
                                ImageSmall = itemGallery.Image,
                                ProductID = model.ProductID,
                                //Product = itemGallery.Name
                            };
                            db.ProductGalleries.InsertOnSubmit(productGallery);
                        }
                        db.SubmitChanges();
                    }


                    return Json(new { success = true, id = model.ProductID });
                }

                catch (Exception ex)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(FormCollection frm , EProduct model2)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = Int32.Parse(frm["ProductID"]);
                    Product model = db.Products.FirstOrDefault(a => a.ProductID == id);
                    if (model == null)
                    {
                        return Json(new { success = false });
                    }
                    model.Alias = frm["Alias"];
                    model.Image = frm["Image"];
                    //model.Image2 = frm["Image2"];
                    //model.Producer = frm["Producer"];
                    model.Technicadata = frm["Technicadata"];
                    //model.Hot = bool.Parse(frm["Hot"]);
                    model.BestSale = bool.Parse(frm["BestSale"]);
                    model.TypeMenuID = frm["TypeMenuID"];
                    model.MenuID = Int32.Parse(frm["MenuID"]);
                    model.ProductName = frm["ProductName"];
                    //model.Price = Math.Round(float.Parse(frm["Price"]), 2);
                    //model.PromotionPrice = Math.Round(float.Parse(frm["PromotionPrice"]), 2);
                    //model.Description = frm["Description"];
                    model.MetaTitle = frm["MetaTitle"];
                    model.MetaDescription = frm["MetaDescription"];
                    model.Content = frm["Content"];
                    model.Categories = frm["Categories"];
                    model.Status = bool.Parse(frm["Status"]);
                    model.TBR = bool.Parse(frm["TBR"]);
                    model.PCR = bool.Parse(frm["PCR"]);
                    model.OTR = bool.Parse(frm["OTR"]);
                    db.SubmitChanges();
                    //xóa gallery 
                    db.ProductGalleries.DeleteAllOnSubmit(db.ProductGalleries.Where(a => a.ProductID == model.ProductID).ToList());
                    //Thêm hình ảnh cho  product
                    if (model2.ProductGalleries != null)
                    {
                        foreach (var itemGallery in model2.ProductGalleries)
                        {
                            ProductGallery productGallery = new ProductGallery
                            {
                                ImageLarge = itemGallery.ImageLarge,
                                ImageSmall = itemGallery.ImageLarge,
                                ProductID = model.ProductID,
                                //Product = itemGallery.Name
                            };
                            db.ProductGalleries.InsertOnSubmit(productGallery);
                        }
                        db.SubmitChanges();
                    }
                    return Json(new { success = true, id = id });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false });

        }

        [HttpPost]
        public JsonResult Delete(int? ProductID)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Product del = db.Products.FirstOrDefault(c => c.ProductID == ProductID);
                    if (del != null)
                    {
                        db.ProductGalleries.DeleteAllOnSubmit(db.ProductGalleries.Where(a => a.ProductID == del.ProductID).ToList());
                        db.Products.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa sản phẩm thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Sản phẩm không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = "Error: " + ex.Message });
            }
        }

        public void LoadData()
        {
            var listMenu = new List<SelectListItem>
        {
        new SelectListItem {Value = "0", Text = "Lựa chọn chuyên mục"}
        };
            var menus = new List<Menu>();

            menus =
            MenuController.GetListMenu(0, Request.Cookies["lang_client"].Value).Where(
            a =>
            a.Type == SystemMenuType.Product || a.Type == SystemMenuType.ProductNew).ToList();

            foreach (Menu menu in menus)
            {
                string sub = "";
                for (int i = 0; i < menu.Level; i++)
                {
                    sub += "|--";
                }
                menu.Title = sub + menu.Title;
            }

            listMenu.AddRange(menus.OrderBy(a => a.Location).Select(a => new SelectListItem
            {
                Text = a.Title,
                Value = a.ID.ToString()
            }).ToList());
            ViewBag.ListMenu = listMenu;
        }

        public void LoadTypeMenu()
        {
            using (var db = new MyDbDataContext())
            {
                List<SelectListItem> listMenu = new List<SelectListItem>();
                listMenu.Add(new SelectListItem() { Value = "0", Text = "--Lựa chọn chuyên mục sản phẩm --" });
                var respones = db.Menus.Where(a => (a.Type == SystemMenuType.Product || a.Type == SystemMenuType.ProductNew) && a.LanguageID == Request.Cookies["lang_client"].Value).ToList();

                foreach (var list in respones)
                {
                    listMenu.Add(new SelectListItem() { Value = list.ID.ToString(), Text = list.Title });
                }

                ViewBag.ListMenu = listMenu;
            }
        }
    }
}