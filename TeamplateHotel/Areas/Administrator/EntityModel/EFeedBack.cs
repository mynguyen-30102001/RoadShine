using System.ComponentModel;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EFeedBack
    {
        public int ID { get; set; }

        public string Image  {get;  set; }

        [DisplayName("Họ tên")]
         public  string FullName { get; set; }

        [DisplayName("Nội dung")]
        public string Content { get; set; }

        [DisplayName("Nhà đầu tư")]

        public string Investors { get; set; }

    }
}