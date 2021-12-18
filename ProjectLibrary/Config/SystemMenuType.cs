using System.Collections.Generic;

namespace ProjectLibrary.Config
{
    public class SystemMenuType
    {
        public const int Home = 1;
        public const int Article = 2;
        public const int Contact = 5;
        //public const int Location = 8;
        public const int OutSite = 9;
        public const int About = 7;
        //public const int Register = 6;
        //public const int Customer = 4;
        //public const int Employee = 12;
        //public const int Features = 13;
        //public const int Dangki = 14;
        public const int Menu = 10;
        public const int Product = 11;
        //public const int Gallery = 12;
        //public const int Promotion = 13;
        public const int Order = 15;
        public const int Faq = 16;
        public const int GetInTouch = 17;
        public const int SpecialOffers = 18;
        public const int ProductNew = 19;
        public const int Why = 20;
        public const int Download = 21;
        public const int TBR = 22;
        public const int PCR = 23;
        public const int OTR = 24;




        public static Dictionary<int, string> CategoryType = new Dictionary<int, string>()
        {
            {Home, "Trang chủ"},
            {Article, "Trang bài viết"},
            {Contact, "Trang liên hệ"},
            //{Location, "Trang vị trí"},
            {About, "Trang giới thiệu"},
            {OutSite, "Trang link ra ngoài"},
            //{Gallery, "Trang Gallery"},
            {Product, "Trang sản phẩm"},
            //{Promotion, "SP Đang Khuyến mãi"},
            {Order, "Order"},
            {Faq, "Faq"},
            {GetInTouch, "GetInTouch"},
            {SpecialOffers, "Ưu đãi"},
            {ProductNew, "Trang sản phẩm mới"},
            {Why, "Why"},
            {Download, "Download"},
            {TBR, "TBR"},
            {PCR, "PCR"},
            {OTR, "OTR"},

            //{Menu, "Menu Thực đơn"},

            //{Price, "Trang giá"},
            //{Register, "Đăng ký"},
            //{Customer, "Khách hàng"},
            //{Employee, "Nhân viên tư vấn"},
            //{Features, "Tính năng"},
            //{Dangki, "Đăng kí"},

        };
    }
}
