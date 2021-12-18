using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Models
{
    public class EF_Product
    {
        public int ProductID { get; set; }
        public string LanguageID { get; set; }
        public string TypeMenuID { get; set; }

        public string ProductName { get; set; }
        public int MenuID { get; set; }
        public string Alias { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }

        public double Price { get; set; }
        public string Content { get; set; }
        public double PromotionPrice { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public bool BestSale { get; set; }

        public string Technicadata { get; set; }

        public string Status { get; set; }
        public int Index { get; set; }
        public string MenuAlias { get; set; }

        public List<Menu> TypeParentID { get; set; }
    }
}