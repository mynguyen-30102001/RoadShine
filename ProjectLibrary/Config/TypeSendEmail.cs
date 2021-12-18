using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ProjectLibrary.Config
{
    public class TypeSendEmail
    {
        public static int Contact = 1;
        public static int Register = 2;
        public static int Reservation = 3;
        public static int Order = 4;


        public static List<ListItem> ListTypeSendEmail()
        {
            var listTypeSendEmail = new List<ListItem>
            {
                new ListItem
                {
                    Value = Contact.ToString(),
                    Text = "Liên hệ",
                },
                new ListItem
                {
                    Value = Register.ToString(),
                    Text = "Đăng ký",
                },
                 new ListItem
                {
                    Value = Reservation.ToString(),
                    Text = "Đặt bàn",
                },
                 new ListItem
                 {
                     Value = Order.ToString(),
                     Text = "Đặt món",
                 },
            };

            return listTypeSendEmail;
        }
    }
}