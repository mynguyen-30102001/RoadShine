using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class ETypeMenu
    {
        public int Id { get; set; }

        [DisplayName("Tên Nhóm Thực Đơn")]
        [Required(ErrorMessage = "Vui lòng nhập tên nhóm thực đơn")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string NameTypeMenu { get; set; }

        [DisplayName("Miêu tả")]
        [Required(ErrorMessage = "Vui lòng nhập Miêu tả")]
        public string Description { get; set; }

        public string Alias { get; set; }
        public bool ShowHome { get; set; }

        [DefaultValue(0)]
        public int Index { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh")]
        public string Image { get; set; }

        public bool Status { get; set; }
    }
}