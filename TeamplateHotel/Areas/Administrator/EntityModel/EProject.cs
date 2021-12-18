using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EProject
    {
        public int PojectID { get; set; }

        [DisplayName("Chuyên mục dự án")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn chuyên mục dự án")]
        public int MenuID { get; set; }

        [DisplayName("Tên dự án")]
        [Required(ErrorMessage = "Vui lòng nhập tên dự án")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Title { get; set; }

        [DisplayName("Alias")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Alias { get; set; }

        [DisplayName("Hình đại diện")]
        [MaxLength(300, ErrorMessage = "Tối đa 300 ký tự")]
        [Required(ErrorMessage = "Vui lòng chọn hình đại diện")]
        public string Image { get; set; }

        public int? Index { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Mô tả chi tiết")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả chi tiết")]
        public string Content { get; set; }

        [DisplayName("Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaTitle { get; set; }

        [DisplayName("Thẻ mô tả")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaDescription { get; set; }


        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }

        [DisplayName("Hiển thị trang chủ")]
        public bool ShowHome { get; set; }


        public List<EGalleryITem> EGalleryITems { get; set; }
    }

    public class EGalleryITem
    {
        public string Image { get; set; }
    }
}
 