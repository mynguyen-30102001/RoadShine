using System;
using System.Collections.Generic;

namespace TeamplateHotel.Models
{
    public class ShowObject
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MenuAlias { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }

        public string Description { get; set; }
        public string Content { get; set; }
        public int? Index { get; set; }
        public DateTime DateTime { get; set; }

        public List<EF_Product> Products { get; set; }

        public int? SortBy { get; set; }
        public int MaximunPrice { get; set; }

        public double Price { get; set; }
        public double? PricePromo { get; set; }
        public string LanguageID { get; set; }
        public string NameProduct { get; set; }
        public int ProductId { get; set; }
        public bool? TBR { get; set; }
        public bool? PCR { get; set; }
        public bool? OTR { get; set; }



    }

}