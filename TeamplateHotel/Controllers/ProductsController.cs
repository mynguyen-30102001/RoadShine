using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public class ProductsController : BasicController
    {
        //
        // GET: /Products/

        [HttpPost]
        public JsonResult FilterResult(EF_Filter filter, int? minximunPrice, int? maximunPrice)
            {
            var db = new MyDbDataContext();
            var query = db.Products;

            var checkmenu = db.Menus.Where(a=> a.Type == SystemMenuType.Product).FirstOrDefault();

            Expression<Func<Product, bool>> predicate = h => h != null;

            if (filter.CategoryListId != null)
            {
                   Expression<Func<Product, bool>> predicatelocation = h => h != null;

                    var location = Int32.Parse(filter.CategoryListId);

                    predicatelocation = (h => (h.MenuID == location ));

                    predicate = predicatelocation;
 
            }
            else
            {

                if (checkmenu.ID == filter.Menuid)
                {
                    predicate = h => h != null;

                }
                else
                {
                    predicate = h => h.MenuID == filter.Menuid;

                }

            }

            if (minximunPrice != null || maximunPrice != null)
            {
                Expression<Func<Product, bool>> predicatePrice = h => h != null;


                predicatePrice = (h => (h.Price >= minximunPrice.Value && h.Price <= maximunPrice.Value));
                predicate = predicatePrice;
            }

            Menu menu = db.Menus.FirstOrDefault(x => x.ID == filter.Menuid);

            //List<Menu> menu = db.Menus.Where(x => x.ID == filter.Menuid).ToList();

            if (filter.CategoryListId != null)
            {
                //var model = query.Where(predicate).Join(db.Menus, a => a.MenuID, b => b.ID, (a, b) => new ShowObject
                var model = query.Where(predicate).Join(db.Menus, a => a.MenuID, b => b.ID, (a, b) => new ShowObject

                {
                    ID = a.ProductID,
                    Alias = a.Alias,
                    MenuAlias = menu.Alias,
                    Image = a.Image,
                    NameProduct = a.ProductName,
                    Image2 = a.Image2,
                    Index = a.Index,
                    PricePromo = a.PromotionPrice,
                    //Price = (a.Price,
                    LanguageID = filter.Language,
                    Title = b.Title,
                }).Where(x => x.LanguageID == filter.Language).Distinct().OrderBy(x => x.Index).ToList();

                if (filter.sort != null)
                {
                    switch (filter.sort)
                    {
                        case 1:
                            model = model.OrderBy(x => x.Price).ToList();
                            break;
                        case 2:
                            model = model.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                var lstproduct = model.ToPagedList(filter.Paging.Page, filter.Paging.PageSize).ToList();


                int totalRow = model.Count;
                return Json(new { data = lstproduct, total = totalRow, status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = query.Where(predicate).Join(db.Menus, a => a.MenuID, b => b.ID, (a, b) => new ShowObject
                {
                    ID = a.ProductID,
                    Alias = a.Alias,
                    MenuAlias = menu.Alias,
                    Image = a.Image,
                    NameProduct = a.ProductName,
                    Image2 = a.Image2,
                    Index = a.Index,
                    PricePromo = a.PromotionPrice,
                    //Price = a.Price,
                    LanguageID = filter.Language,
                    Title = b.Title,
                }).Where(x => x.LanguageID == filter.Language).Distinct().OrderBy(x => x.Index).ToList();

                if (filter.sort != null)
                {
                    switch (filter.sort)
                    {
                        case 1:
                            model = model.OrderBy(x => x.Price).ToList();
                            break;
                        case 2:
                            model = model.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                var lstproduct = model.ToPagedList(filter.Paging.Page, filter.Paging.PageSize).ToList();


                int totalRow = model.Count;
                return Json(new { data = lstproduct, total = totalRow, status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);

        }

        public static IPagedList<EF_Product> ProductsSale(int? page , string languageKey, int menuid)
        {
            using (var db = new MyDbDataContext())
            {
                
                int pageNumber = (page ?? 1);
                int pageSize = 9;

                List<EF_Product> products = db.Products.Where(a => a.Status)
                    .Join(db.Menus.Where(b => b.Status), a => a.MenuID, b => b.ID, (a, b) => new EF_Product
                    {
                        MenuAlias = b.Alias,
                        MenuID = a.MenuID,
                        ProductID = a.ProductID,
                        Alias = a.Alias,
                        Image = a.Image,
                        Image2 = a.Image2,
                        PromotionPrice = (double)a.PromotionPrice,
                        //Price = a.Price,
                        ProductName = a.ProductName,
                        Description = a.Description,
                        Content = a.Content,
                        TypeMenuID = a.TypeMenuID,
                    }).Where(x => x.TypeMenuID.Contains(menuid.ToString())).ToList();

                return products.ToPagedList(pageNumber, pageSize);
            }
        }

    }
}
