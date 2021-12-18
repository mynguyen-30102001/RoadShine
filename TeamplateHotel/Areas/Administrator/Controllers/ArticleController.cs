using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;
using TeamplateHotel.Areas.Administrator.ModelShow;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: /Administrator/Article/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang bài viết";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                var records =
                    db.Articles.Join(db.Menus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
                        a => a.MenuID,
                        b => b.ID, (a, b) => new {a}).ToList();
                foreach (var record in records)
                {
                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.a.ID)];
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
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var listArticle =
                        db.Articles.Join(db.Menus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
                            a => a.MenuID, b => b.ID, (a, b) => new {a, b});
                    List<ShowArticle> records = listArticle.Select(a => new ShowArticle
                    {
                        ID = a.a.ID,
                        Title = a.a.Title,
                        TitleMenu = a.b.Title,
                        Index = a.a.Index,
                        Status = a.a.Status,
                        Home = a.a.Home,
                        About = a.a.About,

                        Hot = a.a.Hot,
                    }).OrderByDescending(a => a.ID).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = records, TotalRecordCount = listArticle.Count()});
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
            LoadData();
            ViewBag.Title = "Thêm bài viết";
            var eArticle = new EArticle();
            return View(eArticle);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EArticle model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Alias);
                    }
                    try
                    {
                        var article = new Article
                        {
                            CreateDate = DateTime.Now,
                            MenuID = model.MenuID,
                            Title = model.Title,
                            Alias = model.Alias,
                            Image = model.Image,
                            Description = model.Description,
                            Content = model.Content,
                            Index = 0,
                            MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle,
                            MetaDescription =
                                string.IsNullOrEmpty(model.MetaDescription) ? model.Title : model.Description,
                            Status = model.Status,
                            Home = model.Home,
                            YouLike = model.YouLike,
                            Hot = model.Hot,
                            About = model.About,
                            Icon = model.Icon,
                            Link = model.Link
                        };

                        db.Articles.InsertOnSubmit(article);
                        db.SubmitChanges();

                        //Thêm hình ảnh cho  product
                        if (model.EGalleryITems != null)
                        {
                            foreach (EArticle.EGalleryITem itemGallery in model.EGalleryITems)
                            {
                                ArticleGallery articleGallery = new ArticleGallery
                                {
                                    ImageLarge = itemGallery.Image,
                                    ImageSmall = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                    ArticleId = article.ID,
                                    //Product = itemGallery.Name
                                };
                                db.ArticleGalleries.InsertOnSubmit(articleGallery);
                            }
                            db.SubmitChanges();
                        }

                        TempData["Messages"] = "Thêm bài viết thành công";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        LoadData();
                        ViewBag.Messages = "Error: " + exception.Message;
                        return View();
                    }
                }
                LoadData();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.Title = "Cập nhật bài viết";
            using (var db = new MyDbDataContext())
            {
                Article detailArticle = db.Articles.FirstOrDefault(a => a.ID == id);

                if (detailArticle == null)
                {
                    TempData["Messages"] = "Bài viết không tồn tại";
                    return RedirectToAction("Index");
                }

                var artile = new EArticle
                {
                    ID = detailArticle.ID,
                    MenuID = detailArticle.MenuID,
                    Title = detailArticle.Title,
                    Alias = detailArticle.Alias,
                    Image = detailArticle.Image,
                    Description = detailArticle.Description,
                    Content = detailArticle.Content,
                    MetaTitle = detailArticle.MetaTitle,
                    MetaDescription = detailArticle.MetaDescription,
                    Status = detailArticle.Status,
                    Home = detailArticle.Home,
                    YouLike = (bool)detailArticle.YouLike,
                    Hot = detailArticle.Hot,
                    Icon = detailArticle.Icon,
                    Link = detailArticle.Link,
                    About = detailArticle.About
                };
                //lấy danh sách hình ảnh
                artile.EGalleryITems = db.ArticleGalleries.Where(a => a.ArticleId == artile.ID).Select(a => new EArticle.EGalleryITem
                {
                    Image = a.ImageLarge
                }).ToList();
                LoadData();
                return View(artile);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(EArticle model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Alias);
                    }
                    try
                    {
                        Article article = db.Articles.FirstOrDefault(b => b.ID == model.ID);
                        if (article != null)
                        {
                            article.MenuID = model.MenuID;
                            article.Title = model.Title;
                            article.Alias = model.Alias;
                            article.Image = model.Image;
                            article.Description = model.Description;
                            article.Content = model.Content;
                            article.MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle;
                            article.MetaDescription = string.IsNullOrEmpty(model.MetaDescription)
                                ? model.Title
                                : model.MetaDescription;
                            article.Status = model.Status;
                            article.Home = model.Home;
                            article.Hot = model.Hot;
                            article.Icon = model.Icon;
                            article.Link = model.Link;
                            article.YouLike = model.YouLike;
                            article.About = model.About;
                            db.SubmitChanges();
                            //xóa gallery cho phòng
                            db.ArticleGalleries.DeleteAllOnSubmit(db.ArticleGalleries.Where(a => a.ArticleId == article.ID).ToList());
                            //Thêm hình ảnh cho phòng
                            if (model.EGalleryITems != null)
                            {
                                foreach (EArticle.EGalleryITem itemGallery in model.EGalleryITems)
                                {
                                    var articleGallery = new ArticleGallery
                                    {
                                        ImageLarge = itemGallery.Image,
                                        ImageSmall = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                        ArticleId = article.ID,
                                    };
                                    db.ArticleGalleries.InsertOnSubmit(articleGallery);
                                }
                                db.SubmitChanges();
                            }
                            TempData["Messages"] = "Cập nhật bài viết thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                        LoadData();
                        model.ArticleGalleries = db.ArticleGalleries.Where(a => a.ArticleId == model.ID).ToList();
                        ViewBag.Messages = "Lỗi: " + exception.Message;
                        return View();
                    }
                }
                LoadData();
                model.ArticleGalleries = db.ArticleGalleries.Where(a => a.ArticleId == model.ID).ToList();
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
                    Article del = db.Articles.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.Articles.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        //xóa hết hình ảnh của article này
                        db.ArticleGalleries.DeleteAllOnSubmit(db.ArticleGalleries.Where(a => a.ArticleId == del.ID).ToList());
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa bài viết thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "Bài viết không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
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
                        a.Type == SystemMenuType.Article ||
                         a.Type == SystemMenuType.About ||
                         a.Type == SystemMenuType.Faq||
                         a.Type == SystemMenuType.SpecialOffers).ToList();

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
    }
}