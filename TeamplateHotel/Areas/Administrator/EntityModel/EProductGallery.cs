using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EProductGallery
    {
        public int GalleryId { get; set; }

        [DisplayName("Tên ảnh")]
        public string TitleGallery { get; set; }

        public int? IndexGallery { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng lựa chọn hình ảnh")]
        public string ImageThump { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng lựa chọn hình ảnh")]
        public string ImageLarge { get; set; }

    }
}