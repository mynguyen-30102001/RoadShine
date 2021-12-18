using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Areas.Administrator.EntityModel;
using TeamplateHotel.Models;


namespace TeamplateHotel.Controllers
{
    public class CommentController : BasicController
    {

        //danh sach ngon ngu
        public static List<Language> GetLanguages()
        {
            using (var db = new MyDbDataContext())
            {
                List<Language> languages = db.Languages.ToList();
                return languages;
            }
        }
        //Chi tiết khách sạn
        public static Company DetailCompany(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                var hotel =
                    db.Companies.FirstOrDefault(a => a.LanguageID == languageKey) ??
                    new Company();
                return hotel;
            }
        }
        //Danh sách Main menu
        public static List<Menu> GetMainMenus(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Menu> menus = db.Menus.Where(a => a.Status && a.Location == SystemMenuLocation.MainMenu &&
                                                       a.LanguageID == languageKey ).OrderBy(a => a.Index).ToList();
                return menus;
            }
        }
        public static List<Gallery> GetListGallery()
        {
            using (var db = new MyDbDataContext())
            {
                List<Gallery> gallarys = db.Galleries.ToList();
                return gallarys;
            }
        }

        public static List<EF_Product> GetProductHot(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {

                List<EF_Product> products = db.Products.Where(a => a.Status && a.Hot)
                 .Join(db.Menus.Where(b => b.Status && b.LanguageID == languageKey), a => a.MenuID, b => b.ID, (a, b) => new EF_Product
                 {
                     MenuAlias = b.Alias,
                     ProductID = a.ProductID,
                     Alias = a.Alias,
                     Image = a.Image,
                     Image2 = a.Image2,
                     PromotionPrice = (double)a.PromotionPrice,
                     //Price = a.Price,
                     ProductName = a.ProductName,
                     Description = a.Description,
                     Content = a.Content,
                     //TypeMenuID = a.TypeMenuID.Split(','),
                 }).ToList();

                return products;
            }
        }

        public static List<BannerLogo> GetBannerLogo(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {

                List<BannerLogo> eMenus = db.BannerLogos.Where(a => a.LanguageID == languageKey && a.Status).ToList();

                return eMenus;
            }
        }

        public static List<EF_Product> GetProductNews(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                List<EF_Product> products = db.Products.Where(a => a.Status && a.BestSale == true)
                    .Join(db.Menus.Where(b => b.Status && b.ID == menuId ), a => a.MenuID, b => b.ID, (a, b) => new EF_Product
                    {
                        MenuAlias = b.Alias,
                        MenuID = a.MenuID,
                        ProductID = a.ProductID,
                        Alias = a.Alias,
                        Image = a.Image,
                        Image2 = a.Image2,
                        //PromotionPrice = (double)a.PromotionPrice,
                        //Price = a.Price,
                        ProductName = a.ProductName,
                        Description = a.Description,
                        Content = a.Content,
                        //TypeMenuID = a.TypeMenuID.Split(','),
                    }).OrderByDescending(x => x.ProductID).ToList();

                return products;
            }
        }
        public static List<Product> GetProductNew()
        {
            using (var db = new MyDbDataContext())
            {
                List<Product> productNew = db.Products.Where(a => a.Status && a.BestSale).ToList();
                    

                return productNew;
            }
        }


        public static List<FeedBack> GetFeedBack(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<FeedBack> feedBacks =
                    db.FeedBacks.Where(a => a.LanguageID == languageKey).OrderBy(a => a.ID).ToList();
                return feedBacks;
            }
        }

        public static List<Banner> GetBanners(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Banner> banners =
                    db.Banners.Where(a => a.LanguageID == languageKey && a.Home).OrderBy(a => a.Index).ToList();
                return banners;
            }
        }

        //Danh sách PR lấy theo Menu
        public static List<Menu> GetMenuProduct(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Menu> menus = db.Menus.Where(a => a.Status && a.Location == SystemMenuLocation.MainMenu &&
                                                       a.LanguageID == languageKey).OrderBy(a => a.Index).ToList();
                return menus;

            }
        }
        //Lấy    theo menu
        public static List<ShowMenuHotels> GMenuandProduct(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                 List<ShowMenuHotels> menuHotelses = db.Menus.Where(a => a.Status && a.LanguageID == languageKey )
                    .Join(db.Products, a => a.ID, b => b.MenuID, (a, b) => new ShowMenuHotels
                    {
                        ID = a.ID,
                        MenuChildAlias = a.Alias,
                        Alias = a.Alias,
                        //MenuAlias = menus.Alias,
                        Type = a.Type,
                        ParentID = a.ParentID,
                        Title = a.Title,
                        MenuID = a.ID,
                        Image = a.Image,
                        Index = a.Index,
                    }).Distinct().OrderBy(x => x.Index).ToList();

                foreach (var item in menuHotelses)
                {
                    List<Product> productstProducts = db.Products.Where(x => x.MenuID == item.MenuID  ).ToList();
                    item.lstProducts = productstProducts;
                  
                 }

                return menuHotelses;
            }
        }

        public static List<T>[] Partition<T>(List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 2)
                throw new ArgumentOutOfRangeException("totalPartitions");

            List<T>[] partitions = new List<T>[totalPartitions];

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }

        //Menu của 1 chuyện mục
        public static List<Menu> MenuDetail(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Menu> detaiMenus = db.Menus.Where(a => a.Location == SystemMenuLocation.MainMenu &&
                                                            a.LanguageID == languageKey).ToList();
                return detaiMenus;
            }
        }
        //Danh sách Second menu
        public static List<Menu> GetSecondMenus(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Menu> menus = db.Menus.Where(a => a.Status && a.Location == SystemMenuLocation.SecondMenu &&
                                                       a.LanguageID == languageKey).OrderBy(a => a.Index).ToList();
                return menus;
            }
        }
        //Danh sách menu theo bài viết
        public static List<Menu> GetMenus(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Menu> menus = db.Menus.Where(a => a.Status == true && a.Type == 2 &&
                                                       a.LanguageID == languageKey).OrderBy(a => a.Index).ToList();
                return menus;
            }
        }
        public static List<Slider> listsiler(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                List<Slider> sliders = db.Sliders.Where(a => a.Status && a.ID == menuId).ToList();
                return sliders;
            }
        }
        //Danh sach slider
        public static List<Slider> GetListSlider(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Slider> sliders = db.Sliders.Where(a => a.LanguageID == languageKey && a.Status).ToList();
                //List<Slider> sliderIsChoise = new List<Slider>();
                //foreach (var slider in sliders)
                //{
                //    int[] menuIds =
                //                slider.MenuIDs.Substring(0, slider.MenuIDs.Length - 1).Split(',').Select(
                //                    n => Convert.ToInt32(n)).ToArray();
                //    if (menuId == 0)
                //    {
                //        //lấy slider ở trang chủ có không ?
                //        var menuHome = db.Menus.FirstOrDefault(a => a.Type == SystemMenuType.Home);
                //        if (menuHome != null)
                //        {
                //            if (menuIds.Contains(menuHome.ID))
                //            {
                //                sliderIsChoise.Add(slider);
                //            }
                //        }
                //        if (sliderIsChoise.Count == 0)
                //        {
                //            sliderIsChoise = sliders;
                //        }
                //    }
                //    else
                //    {
                //        var menu = db.Menus.FirstOrDefault(a => a.ID == menuId);
                //        if (menu != null)
                //        {
                //            if (menuIds.Contains(menu.ID))
                //            {
                //                sliderIsChoise.Add(slider);
                //            }
                //        }
                //        if (sliderIsChoise.Count == 0)
                //        {
                //            sliderIsChoise = sliders;
                //        }
                //    }
                //}
                return sliders;
            }
        }

        //Danh sach quang cao
        public static List<Advertising> GetAdvertisings()
        {
            using (var db = new MyDbDataContext())
            {
                List<Advertising> advertisings = db.Advertisings.Where(a => a.Status).ToList();
                return advertisings;
            }
        }

        //Danh sách bài viết theo chuyên mục
        public static List<Article> GetArticles(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                List<Article> articles = db.Articles.Where(a => a.MenuID == menuId && a.Status).OrderBy(a => a.Index).ToList();
                foreach (var article in articles)
                {
                    article.MenuAlias = article.Menu.Alias;
                }
                return articles;
            }
        }



        //Danh sách bài viết theo chuyên mục
        public static Article detailpost(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                var menu = db.Menus.FirstOrDefault(a => a.ID == menuId);
                Article dpost = db.Articles.Where(a => a.MenuID == menuId && a.Status && menu.Type == 13).FirstOrDefault();

                return dpost;
            }
        }
        //Chi tiết bài viết
        public static DetailArticle Detail_Article(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Article article = db.Articles.FirstOrDefault(a => a.ID == id && a.Status) ?? new Article();
                //List<Article> articles = db.Articles.Where(a => a.MenuID == article.MenuID && a.Status).OrderBy(a => a.Index).ToList();
                List<Article> articlespost = db.Articles.Where(a => a.MenuID == article.MenuID && a.Status && a.ID != article.ID).OrderBy(a => a.Index).ToList();
                List<ArticleGallery> articleGalleries = db.ArticleGalleries.Where(a => a.ArticleId == article.ID).ToList();
                //foreach (var item in articles)
                //{
                //    item.MenuAlias = article.Menu.Alias;
                //}
                DetailArticle detailArticle = new DetailArticle()
                {
                    Article = article,
                    //Articles = articles,
                    Articlespost = articlespost,
                    ArticleGalleries = articleGalleries
                };
                return detailArticle;
            }
        }
        //Danh sách bài viết dự án

        public static List<Poject> GetArticles_Project(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                List<Poject> articles = db.Pojects.Where(a => a.MenuID == menuId && a.Status).OrderBy(a => a.Index).ToList();
                foreach (var article in articles)
                {
                    article.MenuAlias = article.Menu.Alias;
                }
                return articles;
            }
        }

        //Chi tiết bài viết dự án
        public static Detaiproject Detail_Article_Project(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Poject poject = db.Pojects.FirstOrDefault(a => a.PojectID == id && a.Status) ?? new Poject();
                List<Poject> pojects = db.Pojects.Where(a => a.MenuID == poject.MenuID && a.Status).OrderBy(a => a.Index).ToList();
                List<Poject> articlespost = db.Pojects.Where(a => a.MenuID == poject.MenuID && a.Status && a.PojectID != poject.PojectID).OrderBy(a => a.Index).ToList();
                List<PojectGallery> pojectGalleries = db.PojectGalleries.Where(a => a.PojectID == poject.PojectID).ToList();

                foreach (var item in pojects)
                {
                    item.MenuAlias = poject.MenuAlias;
                }
                Detaiproject detaiproject = new Detaiproject()
                {
                     Poject = poject,
                     Pojects = pojects,
                     PojectGalleries = pojectGalleries,
                };
                return detaiproject;
            }
        }
        //HIển thị bài viết
        public static List<ShowObject> GetArticlesLike(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<ShowObject> articles = db.Articles.Where(a => a.Status && (bool)a.YouLike)
                    .Join(db.Menus.Where(a => a.LanguageID == languageKey), a => a.MenuID, b => b.ID,
                        (a, b) => new ShowObject
                        {
                            MenuAlias = b.Alias,
                            ID = a.ID,
                            Alias = a.Alias,
                            Title = a.Title,
                            Index = a.Index,
                            Image = a.Image,
                            Description = a.Description
                        }).Take(5).ToList();
                return articles;
            }
        }

        //Danh sách sản phẩm
        public static List<Product> GetProducts(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                var menu = db.Menus.Where(a => a.ParentID == menuId && a.Status).OrderBy(a => a.Index).ToList();
                List<Product> product = new List<Product>();
                foreach (var item in menu)
                {
                    var listProduct = db.Products.Where(a => a.MenuID == item.ID && a.Status).ToList();
                    
                    product.AddRange(listProduct);
                }


                //List<EF_Product> product2 = db.Products.Where(a => a.Status)
                // .Join(db.Menus.Where(b => b.ID == menuId), a => a.MenuID, b => b.ID, (a, b) => new EF_Product
                // {
                //     MenuAlias = b.Alias,
                //     ProductID = a.ProductID,
                //     Alias = a.Alias,
                //     Image = a.Image,
                //     //PromotionPrice = (double)a.PromotionPrice,
                //     Price = a.Price,
                //     BestSale = a.BestSale,
                //     ProductName = a.ProductName,
                //     Description = a.Description,
                //     Content = a.Content,
                //     Index = a.Index
                //     //TypeMenuID = a.TypeMenuID.Split(','),
                // }).OrderBy(a => a.Index).ToList();

                //foreach (var item in product2)
                //{
                //    List<TypeMenu> listType = new List<TypeMenu>();
                //    var listTypes = item.TypeMenuID;
                //    var lis = db.TypeMenus.Where(x => listTypes.Contains(x.Id.ToString())).ToList();
                //    item.Type = lis;
                //}
                return product;
        }
    }
        public static List<EF_Product> GetProductTBR(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                List<EF_Product> productTBR = db.Products.Where(a => a.Status && a.TBR == true)
                 .Join(db.Menus.Where(b => b.ID == menuId), a => a.MenuID, b => b.ID, (a, b) => new EF_Product
                 {
                     MenuAlias = b.Alias,
                     ProductID = a.ProductID,
                     Alias = a.Alias,
                     Image = a.Image,
                     //PromotionPrice = (double)a.PromotionPrice,
                     //Price = a.Price,
                     BestSale = a.BestSale,
                     ProductName = a.ProductName,
                     Description = a.Description,
                     Content = a.Content,
                     Index = a.Index
                 }).OrderBy(a => a.Index).ToList();

                return productTBR;
            }
        }
        public static List<EF_Product> GetProduct(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                List<EF_Product> product2 = db.Products.Where(a => a.Status)
                 .Join(db.Menus.Where(b => b.ID == menuId), a => a.MenuID, b => b.ID, (a, b) => new EF_Product
                 {
                     MenuAlias = b.Alias,
                     ProductID = a.ProductID,
                     Alias = a.Alias,
                     Image = a.Image,
                     //PromotionPrice = (double)a.PromotionPrice,
                     //Price = a.Price,
                     BestSale = a.BestSale,
                     ProductName = a.ProductName,
                     Description = a.Description,
                     Content = a.Content,
                     Index = a.Index
                     //TypeMenuID = a.TypeMenuID.Split(','),
                 }).OrderBy(a => a.Index).ToList();

                //foreach (var item in product2)
                //{
                //    List<TypeMenu> listType = new List<TypeMenu>();
                //    var listTypes = item.TypeMenuID;
                //    var lis = db.TypeMenus.Where(x => listTypes.Contains(x.Id.ToString())).ToList();
                //    item.Type = lis;
                //}
                return product2;
            }
        }

        //public static List<EF_Product> GetProductsFilter(int menuId, int? sort)
        //{
        //    using (var db = new MyDbDataContext())
        //    {
        //        List<EF_Product> product2 = db.Products.Where(a => a.Status)
        //         .Join(db.Menus.Where(b => b.ID == menuId), a => a.MenuID, b => b.ID, (a, b) => new EF_Product
        //         {
        //             MenuAlias = b.Alias,
        //             ProductID = a.ProductID,
        //             Alias = a.Alias,
        //             Image = a.Image,
        //             PromotionPrice = (double)a.PromotionPrice,
        //             Price = a.Price,
        //             ProductName = a.ProductName,
        //             Description = a.Description,
        //             Content = a.Content,
        //             TypeMenuID = a.TypeMenuID.Split(','),
        //         }).ToList();

        //        foreach (var item in product2)
        //        {
        //            List<TypeMenu> listType = new List<TypeMenu>();
        //            var listTypes = item.TypeMenuID;
        //            var lis = db.TypeMenus.Where(x => listTypes.Contains(x.Id.ToString())).ToList();
        //            item.Type = lis;
        //        }

        //        switch (sort)
        //        {
        //            case 4:
        //                product2 = product2.OrderBy(x => x.Price).ToList();
        //                break;
        //            case 5:
        //                product2 = product2.OrderByDescending(x => x.Price).ToList();
        //                break;
        //        }

        //        return product2;
        //    }
        //}

        //public static EF_TheDishs Detail_TheDish(int id)
        //{
        //    using (var db = new MyDbDataContext())
        //    {
        //        TheDish theDish = db.TheDishes.FirstOrDefault(a => a.Id == id && a.Status);
        //        EF_TheDishs detailsTheDish = new EF_TheDishs()
        //        {
        //            TheDish = theDish,
        //        };
        //        return detailsTheDish;
        //    }
        //}
        //Chi tiết sản phẩm

        public static DetaiProduct GetSortProduct(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Product product = db.Products.FirstOrDefault(a => a.ProductID == id && a.Status) ?? new Product();
                Product lastProduct = db.Products.Where(b => b.ProductID < product.ProductID ).OrderByDescending(a =>a.ProductID).FirstOrDefault();
                Product nextProduct = db.Products.Where(b => b.ProductID > product.ProductID ).OrderBy(a =>a.ProductID).FirstOrDefault();
                //Product nextProduct = db.Products.FirstOrDefault(b => b.ProductID > product.ProductID && b.Status);
                DetaiProduct detaiProduct = new DetaiProduct()
                {
                    Product = product,
                    ProductLast = lastProduct,
                    ProductNext = nextProduct,
                };

                return detaiProduct;
            }
        }

        public static DetaiProduct GetSortProductNew(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Product product = db.Products.FirstOrDefault(a => a.ProductID == id && a.Status && a.BestSale) ?? new Product();
                Product lastProduct = db.Products.Where(b => b.ProductID < product.ProductID && b.BestSale).OrderByDescending(a => a.ProductID).FirstOrDefault();
                Product nextProduct = db.Products.Where(b => b.ProductID > product.ProductID && b.BestSale).OrderBy(a => a.ProductID).FirstOrDefault();
                
                DetaiProduct detaiProduct = new DetaiProduct()
                {
                    Product = product,
                    ProductLast = lastProduct,
                    ProductNext = nextProduct,
                };

                return detaiProduct;
            }
        }

        public static DetaiProduct Detail_Product(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Product product = db.Products.FirstOrDefault(a => a.ProductID == id && a.Status) ?? new Product();
                List<Product> products = db.Products.Where(a => a.MenuID == product.MenuID && a.Status).OrderBy(a => a.Index).ToList();
                List<Product> productsPost = db.Products.Where(a => a.MenuID == product.MenuID && a.Status && a.ProductID != product.ProductID).OrderBy(a => a.Index).ToList();
                List<ProductGallery> productGalleries = db.ProductGalleries.Where(a => a.ProductID == product.ProductID).ToList();
                foreach (var item in products)
                {
                    item.MenuAlias = product.Menu.Alias;
                }
                DetaiProduct detaiProduct = new DetaiProduct()
                {
                    Product = product,
                    Products = products,
                    ProductsPost = productsPost,
                    ProductGalleries = productGalleries,
                    
                };
                return detaiProduct;
            }
        }

        //Danh sách phòng
        public static List<Customer> GetCustomers(int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                var menu = db.Menus.FirstOrDefault(a => a.ID == menuId);
                List<Customer> rooms = db.Customers.Where(a => a.Status && a.LanguageID == menu.LanguageID).OrderBy(a => a.Index).ToList();
                foreach (var room in rooms)
                {
                    room.MenuAlias = menu.Alias;
                }
                return rooms;
            }
        }
        //Chi tiết phòng
        public static DetailCustomer Detail_Customer(int id, int menuId)
        {
            using (var db = new MyDbDataContext())
            {
                Customer customer = db.Customers.FirstOrDefault(a => a.ID == id && a.Status) ?? new Customer();
                List<Customer> customers = db.Customers.Where(a => a.Status && a.ID != customer.ID).OrderBy(a => a.Index).ToList();
                var menu = db.Menus.FirstOrDefault(a => a.ID == menuId);
                foreach (var item in customers)
                {
                    item.MenuAlias = menu.Alias;
                }
                DetailCustomer detailCustomer = new DetailCustomer()
                {
                    Customer = customer,
                    Customers = customers,
                };
                return detailCustomer;
            }
        }
        public static List<Employee> GetEmployee()
        {
            using (var db = new MyDbDataContext())
            {
                List<Employee> empo = db.Employees.
                    ToList();
                return empo;
            }
        }


        ///////////// Trang home ////////////////////////
        //Bai viet welcome
        public static List<ShowObject> ArticleRelated(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<ShowObject> articlerelated =
                    db.Articles.Where(a => a.Status)
                        .Join(db.Menus.Where(a => a.LanguageID == languageKey), a => a.MenuID, b => b.ID,
                            (a, b) => new ShowObject()
                            {
                                ID = a.ID,
                                Alias = a.Alias,
                                MenuAlias = b.Alias,
                                Title = a.Title,
                                Index = a.Index,
                                Image = a.Image,
                                Description = a.Description,
                                DateTime = (DateTime)a.CreateDate
                            }).OrderByDescending(a=>a.ID).ToList();
                return articlerelated;
            }
        }
        //Bai viet hot
        public static List<ShowObject> HotArticles(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<ShowObject> articleHots = db.Articles.Where(a => a.Home && a.Status)
                        .Join(db.Menus.Where(a => a.LanguageID == languageKey), a => a.MenuID, b => b.ID,
                            (a, b) => new ShowObject
                            {
                                ID = a.ID,
                                Alias = a.Alias,
                                MenuAlias = b.Alias,
                                Title = a.Title,
                                Index = a.Index,
                                Image = a.Image,
                                Description = a.Description,
                                DateTime = (DateTime)a.CreateDate,
                            }).ToList();
                return articleHots;
            }
        }

        public static ShowObject GetArticles(string languageKey, int menuid)
        {
            using (var db = new MyDbDataContext())
            {
                ShowObject article = db.Articles.Where(a => a.Status && a.MenuID == menuid)
                    .Join(db.Menus.Where(a => a.LanguageID == languageKey), a => a.MenuID, b => b.ID,
                        (a, b) => new ShowObject
                        {
                            ID = a.ID,
                            Alias = a.Alias,
                            MenuAlias = b.Alias,
                            Title = a.Title,
                            Index = a.Index,
                            Image = a.Image,
                            Description = a.Description,
                            Content = a.Content
                        }).FirstOrDefault();
                return article;
            }
        }
        public static ShowObject AboutArticles(string languageKey , int menuid)
        {
            using (var db = new MyDbDataContext())
            {
                ShowObject articleabout = db.Articles.Where(a => (bool)a.About && a.Status && a.MenuID == menuid)
                    .Join(db.Menus.Where(a => a.LanguageID == languageKey ), a => a.MenuID, b => b.ID,
                        (a, b) => new ShowObject
                        {
                            ID = a.ID,
                            Alias = a.Alias,
                            MenuAlias = b.Alias,
                            Title = a.Title,
                            Index = a.Index,
                            Image = a.Image,
                            Description = a.Description,
                            Content = a.Content
                        }).FirstOrDefault();
                return articleabout;
            }
        }
        public static ShowObject MenuProduct(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                ShowObject product = db.Products.Where(a => a.Status)
                 .Join(db.Menus.Where(b => b.LanguageID == languageKey), a => a.MenuID, b => b.ID, (a, b) => new ShowObject
                 {
                     MenuAlias = b.Alias,
                     ProductId = a.ProductID,
                     Alias = a.Alias,
                     Image = a.Image,
                     //PromotionPrice = (double)a.PromotionPrice,
                     //Price = a.Price,
                     NameProduct = a.ProductName,
                     Description = a.Description,
                     Content = a.Content,
                     Index = a.Index,
                     //TypeMenuID = a.TypeMenuID.Split(','),
                 }).FirstOrDefault();
                return product;
            }
        }
        
        //Menu child
        public static List<Menu> GetMenuChild(string languageKey, int? menuId)
        {
            using (var db = new MyDbDataContext())
            {
                List<Menu> menuChild = db.Menus.Where(a => a.Status && a.LanguageID == languageKey && a.ParentID == menuId).OrderBy(a => a.Index).ToList();
                return menuChild;
            }
        }
        public static List<Menu> GetMenuAboutandArticle(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                List<Menu> menu = db.Menus.Where(a => a.Status && a.LanguageID == languageKey && a.Type == SystemMenuType.About ||a.Type==SystemMenuType.Article).OrderBy(a => a.Index).ToList();
                return menu;
            }
        }
        public static Menu GetMenuAbout(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                Menu menu = db.Menus.Where(a => a.Status && a.LanguageID == languageKey && a.Type == SystemMenuType.About).FirstOrDefault();
                return menu;
            }
        }

        public static Menu GetMenuTBR(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                Menu menu = db.Menus.FirstOrDefault(a => a.Status && a.LanguageID == languageKey && a.Type == SystemMenuType.TBR );
                return menu;
            }
        }
        public static Menu GetMenuPCR(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                Menu menu = db.Menus.FirstOrDefault(a => a.Status && a.LanguageID == languageKey && a.Type == SystemMenuType.PCR);
                return menu;
            }
        }
        public static Menu GetMenuOTR(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                Menu menu = db.Menus.FirstOrDefault(a => a.Status && a.LanguageID == languageKey && a.Type == SystemMenuType.OTR);
                return menu;
            }
        }

        public static Menu ContactMenu(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                Menu menu = db.Menus.FirstOrDefault(a => a.Status && a.LanguageID == languageKey && a.Type == SystemMenuType.Contact);
                return menu;
            }
        }
        //public static List<string> listSize(int productId)
        //{
        //    using (var db = new MyDbDataContext())
        //    {
        //        Product product = db.Products.FirstOrDefault(a => a.Status && a.ProductID == productId);
        //        List<string> listSize = product.Categories.Split(',').ToList();
        //        return listSize;
        //    }
        //}
        public static Menu GetMenuParent(string languageKey, int? menuId)
        {
            using (var db = new MyDbDataContext())
            {
                Menu menu = db.Menus.FirstOrDefault(a => a.Status && a.LanguageID == languageKey && a.ID == menuId);
                Menu menuChild = db.Menus.FirstOrDefault(a => a.Status && a.LanguageID == languageKey && a.ID == menu.ParentID);
                return menuChild;
            }
        }
        public static Menu MenuOffer(string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                Menu menu = new Menu();
                var menuOffer = db.Menus.Any(a => a.Type == SystemMenuType.SpecialOffers);
                if (menuOffer)
                {
                    menu = db.Menus.FirstOrDefault(a => a.LanguageID == languageKey && a.Type == SystemMenuType.SpecialOffers);
                }
                return menu;
            }
        }
    }
}