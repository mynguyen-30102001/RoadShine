using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Models
{
    public class EF_TheDishs
    {
        public string NameTypeMenu { get; set; }

        public string NameMenu { get; set; }

        public int ID { get; set; }

        public string Image { get; set; }

        public string Alias { get; set; }

        public string MenuAlias { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double PromotionPrice { get; set; }

        public bool ShowHome { get; set; }

        public bool Status { get; set; }

        public int Index { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        //public TheDish TheDish { get; set; }
        //public List<TheDish> TheDishs { get; set; }

    }
}