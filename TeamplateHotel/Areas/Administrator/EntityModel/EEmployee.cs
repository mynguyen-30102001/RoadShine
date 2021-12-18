using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EEmployee
    {
        public int Id { get; set; }

        [DisplayName("facebook")]
    
        public string Facebook { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Ảnh đại diện")]
        [MaxLength(300, ErrorMessage = "Tối đa 300 ký tự")]
        [Required(ErrorMessage = "Vui lòng chọn ảnh đại diện")]
        public string Image { get; set; }

        public string Job { get; set; }
        public string Phone { get; set; }

        [DisplayName("Tên nhân viên")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string FullName { get; set; }

     

        [DisplayName("Skype")]
        public string Skype { get; set; }

    }
}