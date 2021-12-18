namespace TeamplateHotel.Models
{
    public class CartItem
    {

        public string Hinh { get; set; }
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string PromotionCode { get; set; }

        public double Rate { get; set; }
 

        public decimal Total { get; set; }

        public decimal ThanhTien
        {
            get
            {
                return SoLuong * DonGia;
            }
        }

    }


}