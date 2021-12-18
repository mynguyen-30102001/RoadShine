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
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult SearchMenu(string keySearch, int? page, int? pageSize)
        {
            using (var db = new MyDbDataContext())
            {
                int pagenumber = page ?? 1;
                int pagesize = pageSize ?? 12;
                ViewBag.KeySearch = keySearch;
                var lan = Request.Cookies["LanguageID"].Value;
                List<ShowObject> result = new List<ShowObject>();
                //search tour
                List<ShowObject> product = db.Products.Where(a => a.Status
                && (a.ProductName.Contains(keySearch)))
                    .Join(db.Menus, a => a.MenuID, b => b.ID, (a, b) => new ShowObject
                    {
                        ProductId = a.ProductID,
                        Alias = a.Alias,
                        MenuAlias = b.Alias,
                        NameProduct = a.ProductName,
                        Image = a.Image,
                        Description = a.Description,
                        TBR = a.TBR,
                        PCR = a.PCR,
                        OTR = a.OTR,
                        Index = a.Index
                    }).OrderBy(a => a.Index).ToList();
                result.AddRange(product);
                if (result == null)
                {
                    return View("/messageSearch");
                }
                //result.Add(new SearchAll
                //{
                //    Tours = tours,
                //    Articles = articles,
                //    ListHotels = listHotels,
                //});
                IPagedList<ShowObject> _list = result.ToPagedList(pagenumber, pagesize);
                return View(_list);
            }
        }

        public ActionResult Messages()
        {
            return View();
        }

    }
}
