using PagedList;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public class HomeController : BasicController
    {

        [HttpGet]
        public ActionResult Index(object aliasMenuSub, object idSub, object aliasSub, int? page, int? pageSizes, int tbr = -1, int pcr = -1, int otr = -1)
        {
            var db = new MyDbDataContext();
            Company hotel = CommentController.DetailCompany(Request.Cookies["LanguageID"].Value);
            ViewBag.MetaTitle = hotel.MetaTitle;
            ViewBag.MetaDesctiption = hotel.MetaDescription;
            if (aliasMenuSub.ToString() == "SelectLanguge")
            {
                Language language = db.Languages.FirstOrDefault(a => a.ID == idSub.ToString());
                if (language == null)
                {
                    language = db.Languages.FirstOrDefault();
                }
                HttpCookie langCookie = Request.Cookies["LanguageID"];
                langCookie.Value = language.ID;
                langCookie.Expires = DateTime.Now.AddDays(10);
                HttpContext.Response.Cookies.Add(langCookie);
                return Redirect("/");
            }

            //var listTypeMenu = db.TypeMenus.Where(x => x.Status).ToList();
            //foreach (var item in listTypeMenu)
            //{
            //    if (item.Alias == aliasMenuSub.ToString())
            //    {
            //        if (idSub.ToString() != "System.Object")
            //        {
            //            int idArticle;
            //            int.TryParse(idSub.ToString(), out idArticle);
            //            EF_TheDishs detailTheDish = CommentController.Detail_TheDish(idArticle);
            //            ViewBag.MetaTitle = detailTheDish.TheDish.MetaTitle;
            //            ViewBag.MetaDesctiption = detailTheDish.TheDish.MetaDescription;
            //            return View("Product/DetailsTheDish", detailTheDish);
            //        }
            //        var listTheDish = CommentController.GetTheDishesMenu2(item.Alias);
            //        ViewData["ListTheDish"] = listTheDish;
            //        ViewBag.Title = item.Alias;
            //        return View("Product/ListTheDish");
            //    }
            //}
            // xác định menu => tìm ra Kiểu hiển thị của menu
            Menu menu = db.Menus.FirstOrDefault(a => a.Alias == aliasMenuSub.ToString());
            if (aliasMenuSub.ToString() == "System.Object")
            {
                ViewBag.HomePage = true;
                ViewBag.Menu = db.Menus.FirstOrDefault(a => a.Type == SystemMenuType.Home);
                return View("Index");
            }
            if (menu == null)
            {
                return RedirectToRoute(new { controller = "Error", action = "NotFound" });
            }
            //Seo
            ViewBag.MetaTitle = menu.MetaTitle;
            ViewBag.MetaDesctiption = menu.MetaDescription;
            ViewBag.Menu = menu;

            switch (menu.Type)
            {
                case SystemMenuType.About:
                    return View("AboutUS");
                case SystemMenuType.GetInTouch:
                    return View("GetInTouch");
                case SystemMenuType.Faq:
                    return View("Faq");
                case SystemMenuType.Article:
                    goto Trangbaiviet;
                case SystemMenuType.Order:
                    return View("Order");
                case SystemMenuType.Download:
                    return View("Download");
                case SystemMenuType.Why:
                    return View("WhyRoadShine");
                case SystemMenuType.SpecialOffers:
                    goto Trangoffer;
                //case SystemMenuType.Project:
                //    goto Trangduan;
                //case SystemMenuType.Gallery:
                //    return View("Gallery");
                case SystemMenuType.Product:
                    goto Trangsanpham;
                case SystemMenuType.ProductNew:
                    goto Trangsanphammoi;
                //case SystemMenuType.Promotion:
                //    return View("Promotion", ProductsController.ProductsSale(page, Request.Cookies["LanguageID"].Value, menu.ID));
                //case SystemMenuType.Dangki:
                //return View("ThongBao");
                case SystemMenuType.Contact:
                    return View("Contact");
                //case SystemMenuType.Employee:
                //    return View("Employee");
                //case SystemMenuType.Features:
                //    goto Trangbaiviet;
                //case SystemMenuType.Customer:
                //    goto TrangCustomer;
                default:
                    return View("Index");
            }


        #region "Trang dự án"

        Trangduan:
            if (idSub.ToString() != "System.Object")
            {
                int idArticle;
                int.TryParse(idSub.ToString(), out idArticle);
                Detaiproject detaiproject = CommentController.Detail_Article_Project(idArticle);
                ViewBag.MetaTitle = detaiproject.Poject.MetaTitle;
                ViewBag.MetaDesctiption = detaiproject.Poject.MetaDescription;
                return View("Project/DetailPoject", detaiproject);
            }
            //Danh sách bài viết
            var project = CommentController.GetArticles_Project(menu.ID);
            if (project.Count == 1)
            {
                Detaiproject detaiproject = CommentController.Detail_Article_Project(project[0].PojectID);
                ViewBag.MetaTitle = detaiproject.Poject.MetaTitle;
                ViewBag.MetaDesctiption = detaiproject.Poject.MetaDescription;
                return View("Project/DetailPoject", detaiproject);
            }
            int pageNumber = page ?? 1;
            int pageSize = 5;
            return View("Project/Index", project.ToPagedList(pageNumber, pageSize));
        //return View("Article/ListArticle", articles);

        #endregion



        #region "Trang bài viết"

        Trangbaiviet:
            if (idSub.ToString() != "System.Object")
            {
                int idArticle;
                int.TryParse(aliasSub.ToString(), out idArticle);
                DetailArticle detailArticle = CommentController.Detail_Article(idArticle);
                ViewBag.MetaTitle = detailArticle.Article.MetaTitle;
                ViewBag.MetaDesctiption = detailArticle.Article.MetaDescription;
                return View("Article/DetailArticle", detailArticle);
            }
            //Danh sách bài viết
            var articles = CommentController.GetArticles(menu.ID);
            if (articles.Count == 1)
            {
                DetailArticle detailArticle = CommentController.Detail_Article(articles[0].ID);
                ViewBag.MetaTitle = detailArticle.Article.MetaTitle;
                ViewBag.MetaDesctiption = detailArticle.Article.MetaDescription;
                return View("Article/DetailArticle", detailArticle);
            }
            int pageNumber1 = page ?? 1;
            int pageSize1 = pageSizes ?? 4;
            IPagedList<Article> _list = articles.ToPagedList(pageNumber1, pageSize1);
            return View("Article/ListArticle", _list);
        //return View("Article/ListArticle", articles);

        #endregion

        #region "Trang sản phẩm"
        Trangsanpham:
            if (idSub.ToString() != "System.Object")
            {
                int idProduct;
                int.TryParse(idSub.ToString(), out idProduct);
                DetaiProduct detaiProduct = CommentController.Detail_Product(idProduct);
                ViewBag.MetaTitle = detaiProduct.Product.ProductName;
                ViewBag.MetaDesctiption = detaiProduct.Product.Description;
                return View("Product/DetailProduct", detaiProduct);
            }
            
            //Danh sách sp
            var products = CommentController.GetProducts(menu.ID);
           
            //if (products.Count == 1)
            //{
            //    DetaiProduct detaiProduct = CommentController.Detail_Product(products[0].ProductID);
            //    ViewBag.MetaTitle = detaiProduct.Product.ProductName;
            //    ViewBag.MetaDesctiption = detaiProduct.Product.Description;
            //    return View("Product/DetailProduct", detaiProduct);
            //}
            //int pageNumber = page ?? 1;
            //int pageSize = 10;
            //return View("Product/ListProduct", products.ToPagedList(pageNumber, pageSize));
            ShowObject model = new ShowObject();
            //model.MaximunPrice = CommentController.GetMaximunPrice();
            ViewBag.Language = menu.LanguageID;
            ViewBag.MenuID = menu.ID;
            return View("Product/ListProduct", model);
        #endregion
        #region "Trang sản phẩm mới"
        Trangsanphammoi:
            if (idSub.ToString() != "System.Object")
            {
                int idProduct;
                int.TryParse(idSub.ToString(), out idProduct);
                DetaiProduct detaiProduct = CommentController.Detail_Product(idProduct);
                ViewBag.MetaTitle = detaiProduct.Product.ProductName;
                ViewBag.MetaDesctiption = detaiProduct.Product.Description;
                return View("ProductNew/DetailProductNew", detaiProduct);
            }

            //Danh sách sp
            var productnew = CommentController.GetProductNew();

            //if (products.Count == 1)
            //{
            //    DetaiProduct detaiProduct = CommentController.Detail_Product(products[0].ProductID);
            //    ViewBag.MetaTitle = detaiProduct.Product.ProductName;
            //    ViewBag.MetaDesctiption = detaiProduct.Product.Description;
            //    return View("Product/DetailProduct", detaiProduct);
            //}
            ////int pageNumber = page ?? 1;
            ////int pageSize = 10;
            ////return View("Product/ListProduct", products.ToPagedList(pageNumber, pageSize));
            ShowObject modelNew = new ShowObject();
            //model.MaximunPrice = CommentController.GetMaximunPrice();
            ViewBag.Language = menu.LanguageID;
            ViewBag.MenuID = menu.ID;
            return View("ProductNew/ListProductNew", modelNew);
        #endregion
        #region "Trang order"
        Trangorder:
            if (idSub.ToString() != "System.Object")
            {
                int idProduct;
                int.TryParse(idSub.ToString(), out idProduct);
                DetaiProduct detaiProduct = CommentController.Detail_Product(idProduct);
                ViewBag.MetaTitle = detaiProduct.Product.ProductName;
                ViewBag.MetaDesctiption = detaiProduct.Product.Description;
                return View("Product/DetailProduct", detaiProduct);
            }
            //Danh sách sp
            var productss = CommentController.GetProducts(menu.ID);
            //if (products.Count == 1)
            //{
            //    DetaiProduct detaiProduct = CommentController.Detail_Product(products[0].ProductID);
            //    ViewBag.MetaTitle = detaiProduct.Product.ProductName;
            //    ViewBag.MetaDesctiption = detaiProduct.Product.Description;
            //    return View("Product/DetailProduct", detaiProduct);
            //}
            //int pageNumber = page ?? 1;
            //int pageSize = 10;
            //return View("Product/ListProduct", products.ToPagedList(pageNumber, pageSize));
            ShowObject models = new ShowObject();
            //models.MaximunPrice = CommentController.GetMaximunPrice();
            ViewBag.Language = menu.LanguageID;
            ViewBag.MenuID = menu.ID;
            return View("Product/ListProduct", models);
        #endregion
        #region "Trang ưu đãi"

        Trangoffer:
            if (idSub.ToString() != "System.Object")
            {
                int idArticle;
                int.TryParse(aliasSub.ToString(), out idArticle);
                DetailArticle detailArticle = CommentController.Detail_Article(idArticle);
                ViewBag.MetaTitle = detailArticle.Article.MetaTitle;
                ViewBag.MetaDesctiption = detailArticle.Article.MetaDescription;
                return View("Article/DetailArticle", detailArticle);
            }
            //Danh sách bài viết
            var offerAricles = CommentController.GetArticles(menu.ID);
            if (offerAricles.Count == 1)
            {
                DetailArticle detailArticle = CommentController.Detail_Article(offerAricles[0].ID);
                ViewBag.MetaTitle = detailArticle.Article.MetaTitle;
                ViewBag.MetaDesctiption = detailArticle.Article.MetaDescription;
                return View("Article/DetailArticle", detailArticle);
            }
            //int pageNumber1 = page ?? 1;
            //int pageSize1 = 5;
            return View("SpecialOffers", offerAricles);
            //return View("Article/ListArticle", articles);

            #endregion
        }

        public ActionResult ToContact()
        {
            TempData["check"] = "check";
            return View("Contact");
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View("Contact");
        }
        [HttpPost]
        public ActionResult SendContact(Contact model)
        {

            using (var db = new MyDbDataContext())
            {
                model.CreateDate = DateTime.UtcNow;
                try
                {
                    db.Contacts.InsertOnSubmit(model);
                    db.SubmitChanges();
                    SendEmail sendEmail =
                        db.SendEmails.FirstOrDefault(
                            a => a.Type == TypeSendEmail.Contact && a.LanguageID == Request.Cookies["LanguageID"].Value);
                    Company company = CommentController.DetailCompany(Request.Cookies["LanguageID"].Value);

                    string content = sendEmail.Content;
                    content = content.Replace("{FullName}", model.FullName);
                     content = content.Replace("{Tel}", model.Tel);
                     content = content.Replace("{Country}", model.Country);
                    content = content.Replace("{Email}", model.Email);
                    content = content.Replace("{Request}", model.Request);

                    content = content.Replace("{AddressHotel}", company.Address);
                    content = content.Replace("{NameHotel}", company.Name);
                    content = content.Replace("{TelHotel}", company.Tel);
                    content = content.Replace("{EmailHotel}", company.Email);
                    content = content.Replace("{Website}", company.Website);

                    MailHelper.SendMail(model.Email, sendEmail.Title, content);
                    MailHelper.SendMail(company.Email, sendEmail.Title + model.FullName, content);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Redirect("Messages?status=error");
                }
                return Redirect("contact/Messages?status=success");
            }

        }
        [HttpGet]
        public ActionResult Messages()
        {
            using (var db = new MyDbDataContext())
            {
                SendEmail sendEmail =
                    db.SendEmails.FirstOrDefault(
                        a => a.Type == TypeSendEmail.Contact && a.LanguageID == Request.Cookies["LanguageID"].Value);

                string status = Request.Params["status"];
                ViewBag.Messages = "";
                if (string.IsNullOrEmpty(status) == false)
                {
                    if (status.Equals("success"))
                    {
                        ViewBag.Messages = sendEmail.Success;
                    }
                    else
                    {
                        ViewBag.Messages = sendEmail.Error;
                    }
                }
                else
                {
                    ViewBag.Messages = sendEmail.Error;
                }
                return View();
            }
        }
        public ActionResult DestroySession()
        {
            Session["giohang"] = null;
            return Redirect(Request.UrlReferrer.ToString());

        }

        //[HttpPost]
        //public ActionResult ContactHome(Contact model)
        //{
        //    model.CreateDate = DateTime.Now;
        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new MyDbDataContext())
        //        {
        //            db.Contacts.InsertOnSubmit(model);
        //            db.SubmitChanges();

        //            SendEmail sendEmail =
        //                db.SendEmails.FirstOrDefault(
        //                    a => a.Type == TypeSendEmail.Contact && a.LanguageID == Request.Cookies["LanguageID"].Value);
        //            Company company = CommentController.DetailCompany(Request.Cookies["LanguageID"].Value);

        //            string content = sendEmail.Content;
        //            content = content.Replace("{Name}", model.Name);
        //            content = content.Replace("{Tel}", model.Tel);
        //            content = content.Replace("{Email}", model.Email);
        //            content = content.Replace("{Subject}", model.Subject);
        //            content = content.Replace("{Message}", model.Message);




        //            MailHelper.SendMail(company.Email, sendEmail.Title + model.Name, content);
        //            return Redirect("/Messages?status=success");
        //        }
        //    }
        //    return Redirect("/Contact/Messages?status=error");
        //}
        
        public ActionResult FilterProduct(int id, int? page, int? pageSizes, string menuAlias, int tbr = 0, int pcr = 0, int otr = 0)
        {
            var db = new MyDbDataContext();
            var menus = db.Menus.Where(a => a.Status && a.ID == id).FirstOrDefault();
            List<ShowObject> product = new List<ShowObject>();
            int pagenumber = page ?? 1;
            int pagesize = pageSizes ?? 12;
            menuAlias = menus.Alias;
            if (menus.ParentID == 0)
            {
                var menu = db.Menus.Where(a => a.ParentID == id && a.Status).OrderBy(a => a.Index).ToList();
                List<ShowObject> productfilter = new List<ShowObject>();
                foreach (var item in menu)
                {
                    var listProduct = db.Products.Where(a => a.MenuID == item.ID && a.Status)
                        .Join(db.Menus.Where(b => b.Status && b.ID ==item.ID), a => a.MenuID, b => b.ID, (a, b) => new ShowObject {
                            ProductId = a.ProductID,
                            MenuAlias = menuAlias,
                            Alias = a.Alias,
                            Image = a.Image,
                            NameProduct = a.ProductName,
                            Description = a.Description,
                            Content = a.Content,
                            TBR = a.TBR,
                            PCR = a.PCR,
                            OTR = a.OTR,
                            Index = a.Index
                        }).OrderBy(a => a.Index).ToList();
                    productfilter.AddRange(listProduct);
                }
                product = productfilter;
                if (tbr == 1)
                {
                    product = productfilter.Where(a => a.TBR == true).ToList();
                }
                if (pcr ==1)
                {
                    product = productfilter.Where(a => a.PCR == true).ToList();
                }
                if (otr ==1 )
                {
                    product = productfilter.Where(a => a.OTR == true).ToList();
                }
            }
            else
            {
                List<ShowObject> productfilter = new List<ShowObject>();
                product = db.Products.Where(a => a.Status)
                 .Join(db.Menus.Where(b => b.ID == id), a => a.MenuID, b => b.ID, (a, b) => new ShowObject
                 {
                     MenuAlias = menuAlias,
                     ProductId = a.ProductID,
                     Alias = a.Alias,
                     Image = a.Image,
                     NameProduct = a.ProductName,
                     Description = a.Description,
                     Content = a.Content,
                     TBR = a.TBR,
                     PCR = a.PCR,
                     OTR = a.OTR,
                     Index = a.Index
                     //TypeMenuID = a.TypeMenuID.Split(','),
                 }).OrderBy(a => a.Index).ToList();
                productfilter = product;
                if (tbr == 1)
                {
                    product = productfilter.Where(a => a.TBR == true).ToList();
                }
                if (pcr == 1)
                {
                    product = productfilter.Where(a => a.PCR == true).ToList();
                }
                if (otr == 1)
                {
                    product = productfilter.Where(a => a.OTR == true).ToList();
                }

            }
            IPagedList<ShowObject> _list = product.ToPagedList(pagenumber, pagesize);
            TempData["tbr"] = tbr;
            TempData["pcr"] = pcr;
            TempData["otr"] = otr;
            TempData["menuAlias"] = menuAlias;
            return View("Product/FilterProduct", _list);

        }

        
    }
}