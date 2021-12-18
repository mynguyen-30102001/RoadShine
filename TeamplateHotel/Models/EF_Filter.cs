using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using TeamplateHotel.Controllers;

namespace TeamplateHotel.Models
{

    public class EF_Filter
    {
        public EF_Filter()
        {
            Paging = new Paging();
        }

        public string Language { get; set; }
        public int Menuid { get; set; }
        public string MenuAlias { get; set; }
        public string Alias { get; set; }

        public string CategoryListId { get; set; }
        public int? sort { get; set; }
        public List<string> Themes { get; set; }

        public List<string> Star { get; set; }

        public Paging Paging { get; set; }
    }
    public class Paging
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }


}


