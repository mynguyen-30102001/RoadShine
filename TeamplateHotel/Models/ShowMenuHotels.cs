using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Models
{
    public class ShowMenuHotels
    {
        public int MenuID{ get; set; }
        public int ID { get; set; }
        public int? ParentID { get; set; }

        public bool Status { get; set; }
        public bool ShowHome { get; set; }
        public string MenuAlias { get; set; }
        public string MenuChildAlias { get; set; }

        public string  Alias { get; set; }
        public int Type { get; set; }

        public string Title { get; set; }
        public int? Index { get; set; }


        public string Image { get; set; }
        public string Description { get; set; }

        public List<Product> lstProducts { get; set; }
     }
}