using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class ETheMenu
    {

        public int ProductID { get; set; }
        public int menuId { get; set; }

        public int Type { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thực đơn")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string NameMenu { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Miêu tả")]
        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public double PromotionPrice { get; set; }

        public bool ShowHome { get; set; }
 
        public bool Status { get; set; }
        public bool? BestSale { get; set; }

        public string Alias { get; set; }
        public string MetaTitle { get; set; }
        public  string MetaDescription { get; set; }

        public  int Index { get; set; }
        public  string NameTypeMenu { get; set; }

}
}