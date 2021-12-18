using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string LanguageID { get; set; }

        public string[] TypeMenuID { get; set; }

        [DisplayName("Chuyên mục")]
        [Required(ErrorMessage = "Vui lòng chọn chuyên mục")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn chuyên mục")]
        public int MenuID { get; set; }

        public string Alias { get; set; }

        [DisplayName("Ảnh đại diện")] public string Image { get; set; }

        [DisplayName("Ảnh khi hover")] public string Image2 { get; set; }

        public string Producer { get; set; }

        public string Technicadata { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public float Price { get; set; }

        public float PromotionPrice { get; set; }

        public string Description { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string Content { get; set; }

        public string Categories { get; set; }
        public bool Status { get; set; }

        public bool Hot { get; set; }
        public bool? TBR { get; set; }
        public bool? PCR { get; set; }
        public bool? OTR { get; set; }
        public bool BestSale { get; set; }
        public int Index { get; set; }

        public List<EGalleryITem> EGalleryITems { get; set; }

        public List<ProductGallery> ProductGalleries { get; set; }

        public class EGalleryITem
        {
            public int ProductGalleryId { get; set; }
            public int ProductID { get; set; }
            public string ImageLarge { get; set; }
            public string ImageSmall { get; set; }
            public string Image { get; set; }

        }
    }
}