using System;
using ProjectLibrary.Database;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);

        }

        public JsonResult DanhSach()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;

            var count = 0;

            if (giohang != null)  
            {
                 count = giohang.Count();
            }
            else
            {
                count = 0;
            }

            return Json(new { data = giohang, totalp = count, status = true },JsonRequestBehavior.AllowGet);


        }

        public JsonResult ThemVaoGio(int sanPhamID, int? newQuantity)
        {

            if (newQuantity == null)
            {
                newQuantity = 1;
            }
            var db = new MyDbDataContext();
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>(); // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>; // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.SanPhamID == sanPhamID) == null) // ko co sp nay trong gio hang
            {
                Product sp = db.Products.FirstOrDefault(a => a.ProductID == sanPhamID); // tim sp theo sanPhamID

                decimal Price;

                if (sp.PromotionPrice != null && sp.PromotionPrice > 0)
                {
                    Price = (decimal)sp.PromotionPrice;
                }
                else
                {
                    Price = (decimal)sp.Price;
                }
                CartItem newItem = new CartItem()
                {
                    SanPhamID = sanPhamID,
                    TenSanPham = sp.ProductName,
                    SoLuong = (int)newQuantity,
                    Hinh = sp.Image, 
                    DonGia =  Price,
 


                }; // Tạo ra 1 CartItem mới

                giohang.Add(newItem); // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == sanPhamID);
                cardItem.SoLuong = cardItem.SoLuong + (int)newQuantity;
            }

            var count = giohang.Count();

           //return RedirectToAction("Index", "Cart", new { id = sanPhamID });
            return Json(new { data = giohang, totalp = count, status = true });


        }

        public JsonResult SuaSoLuong(int sanPhamID, int newQuantity)
        {
            // tìm carditem muon sua
            bool result = false;
            CartItem itemSua = null;
            string total = "0";
            string itemTotal = "0";
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            if (giohang != null)
            {
                itemSua = giohang.FirstOrDefault(m => m.SanPhamID == sanPhamID);
                if (itemSua != null)
                {
                    itemSua.SoLuong = newQuantity;
                    itemTotal = itemSua.ThanhTien.ToString();
                }

                total = giohang.Sum(m => m.ThanhTien).ToString("#,##0").Replace(',', '.');
                result = true;

            }
            Session["giohang"] = giohang;
            return Json(new { itemTotal , total, status = result}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaKhoiGio(int sanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == sanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            //return RedirectToAction("Index");
            return Json(new { data = giohang,  status = true }, JsonRequestBehavior.AllowGet);

        }


      
    }
}
