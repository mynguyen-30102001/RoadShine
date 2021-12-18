using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        //[HttpPost]
        public ActionResult Index(Booking model)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var order = new Order();
                    order.DateBook = DateTime.UtcNow;

                    double rate = 0;
                    DateTime today = DateTime.UtcNow;
                    //if (string.IsNullOrEmpty(model.PromotionCode) == false)
                    //{
                    //    PromotionCode Pcode = db.PromotionCodes.FirstOrDefault(c => c.Code == model.PromotionCode && today <= c.EndDay && today >= c.StartDay && c.Status == true);

                    //    if (Pcode != null)
                    //    {
                    //        /*
                    //        1-het ma
                    //        2-ok co ma va con luot dung
                    //        3-loi ko co ma nao
                    //        */
                    //        if (Pcode.Used >= Pcode.Total)
                    //        {
                    //            rate = 0;
                    //        }
                    //        else
                    //        {
                    //            rate = Pcode.Rate / 100;
                    //            Pcode.Used = Pcode.Used + 1;
                    //            db.SubmitChanges();
                    //        }
                    //    }
                    //}
                    List<CartItem> giohang = Session["giohang"] as List<CartItem>;
                    foreach (var item in giohang)
                    {
                        //item.PromotionCode = model.PromotionCode;
                        //item.Rate = rate;

                        item.Total = item.ThanhTien;
                        //break;
                    }
                    List<ListItemCart> listItemCarts = giohang.Select(a => new ListItemCart()
                    {
                        PID = a.SanPhamID,
                        PName = a.TenSanPham,
                        PQuantity = a.SoLuong,
                        Subtotal = a.ThanhTien,
                        PPrice = a.ThanhTien,
                        CodePromo = a.PromotionCode,
                        Total = a.Total,
                    }).ToList();

                    order.ListItemCarts = listItemCarts;

                    return View(order);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //[HttpGet]
        //public ActionResult Index()
        //{

        //    // var order = new Order();
        //    // order.DateBook = DateTime.Now;
        //    // List<CartItem> giohang = Session["giohang"] as List<CartItem>;

        //    // List<ListItemCart> listItemCarts = giohang.Select(a => new ListItemCart()
        //    //     {
        //    //         PID = a.SanPhamID,
        //    //         PName = a.TenSanPham,
        //    //         PQuantity = a.SoLuong, 
        //    //         PPrice = a.DonGia,
        //    //     }).ToList();

        //    //order.ListItemCarts = listItemCarts;

        //    //return View(order);
        //}



        [HttpPost]
        public ActionResult SendBooking(Order model, string productDetails, string material)
        {
            string status = "success";

            try
            {
                using (var db = new MyDbDataContext())
                {

                    Company hotel = CommentController.DetailCompany(Request.Cookies["LanguageID"].Value);
                    string codeBooking = hotel.CodeBooking + "1";
                    if (db.Orders.Any())
                    {
                        codeBooking = hotel.CodeBooking +
                                      (db.Orders.OrderByDescending(a => a.ID).FirstOrDefault().ID + 1);
                    }
                    model.Code = codeBooking;
                    //string codePromo = "";

                    //List<CartItem> giohang = Session["giohang"] as List<CartItem>;

                    //List<ListItemCart> listItemCarts = giohang.Select(a => new ListItemCart()
                    //{
                    //    PID = a.SanPhamID,
                    //    PName = a.TenSanPham,
                    //    PQuantity = a.SoLuong,
                    //    PPrice = a.DonGia,
                    //    Subtotal = a.ThanhTien,
                    //    //CodePromo = a.PromotionCode,
                    //    Total = a.Total,

                    //}).ToList();

                    //foreach (var item in listItemCarts)
                    //{
                    //    if (item.PQuantity > 0)
                    //    {
                    //        infoBooking += "\n" + "[" +item.PName + " x " + item.PQuantity + ",Đơn giá : $" + item.PPrice +"]" ;
                    //        totelPrice +=  (decimal)item.Subtotal;
                    //        totalPrice += item.Total;
                    //        //codePromo = item.CodePromo;

                    //        Console.Write(totelPrice);
                    //    }
                    //}

                    //foreach (ListItemCart item in model.ListItemCarts)
                    //{
                    //    if (item.Rate != null)
                    //    {
                    //        totalPrice = totelPrice * (decimal)item.Rate;
                    //    }
                    //}
                    model.Jacket = productDetails == "Jacket" ? true : false;
                    model.Vest = productDetails == "Vest" ? true : false;
                    model.Pants = productDetails == "Pants" ? true : false;
                    model.Shirt = productDetails == "Shirt" ? true : false;
                    model.Coat = productDetails == "Coat" ? true : false;
                    model.Suit = productDetails == "Suit" ? true : false;
                    model.AllProduct = productDetails == "AllProduct" ? true : false;
                    model.Material = material;
                    db.Orders.InsertOnSubmit(model);
                    db.SubmitChanges();
                    //Gửi email xác nhận đặt phòng
                    SendEmail sendEmail =
                    db.SendEmails.FirstOrDefault(
                        a => a.Type == TypeSendEmail.Order && a.LanguageID == Request.Cookies["LanguageID"].Value);

                    sendEmail.Title = sendEmail.Title.Replace("{HotelName}", hotel.Name);
                    string content = sendEmail.Content;
                    content = content.Replace("{Code}", model.Code);
                    //content = content.Replace("{Gender}", model.Gender);
                    content = content.Replace("{FirstName}", model.FirstName);
                    content = content.Replace("{LastName}", model.LastName);

                    content = content.Replace("{Email}", model.Email);
                    content = content.Replace("{Tel}", model.Phone);
                    content = content.Replace("{Address}", model.Address);
                    content = content.Replace("{Country}", model.Country);
                    content = content.Replace("{DateBook}", model.DateBook.ToString());
                    content = content.Replace("{CompletionDate}", model.CompletionDate.ToString());
                    //ProductDetail
                    content = productDetails == "Jacket" ? content.Replace("{ProductDetails}", "Jacket")
                    : productDetails == "Vest" ? content.Replace("{ProductDetails}", "Vest")
                    : productDetails == "Pants" ? content.Replace("{ProductDetails}", "Pants")
                    : productDetails == "Shirt" ? content.Replace("{ProductDetails}", "Shirt")
                    : productDetails == "Coat" ? content.Replace("{ProductDetails}", "Coat")
                    : productDetails == "Suit" ? content.Replace("{ProductDetails}", "Suit")
                    : productDetails == "AllProduct" ? content.Replace("{ProductDetails}", "AllProduct") : "";
                    //Jacket
                    content = content.Replace("{QuantityJacket}", model.QuantityJacket.ToString());
                    content = content.Replace("{FitJacket}", model.FitJacket);
                    content = content.Replace("{ButtonJacket}", model.ButtonJacket);
                    content = content.Replace("{LapelJacket}", model.LapelJacket);
                    content = content.Replace("{PocketJacket}", model.PocketJacket);
                    content = content.Replace("{VentsJacket}", model.VentsJacket);
                    content = content.Replace("{BreastedJacket}", model.BreastedJacket);
                    //Pants
                    content = content.Replace("{QuantityPants}", model.QuantityPants.ToString());
                    content = content.Replace("{FitPants}", model.FitPants);
                    content = content.Replace("{PleatPants}", model.PleatPants);
                    content = content.Replace("{BackPocket}", model.BackPocket);
                    content = content.Replace("{PocketPants}", model.PocketPants);
                    content = content.Replace("{CuffPants}", model.CuffPants);
                    //Vest
                    content = content.Replace("{QuantityVest}", model.QuantityVest.ToString());
                    content = content.Replace("{NumberOfButton}", model.NumberOfButton);
                    content = content.Replace("{BreastedVest}", model.BreastedVest);
                    //Shirt
                    content = content.Replace("{QuantityShirt}", model.QuantityShirt.ToString());
                    content = content.Replace("{FitShirt}", model.FitShirt);
                    content = content.Replace("{CollarShirt}", model.CollarShirt);
                    content = content.Replace("{PocketShirt}", model.PocketShirt);
                    content = content.Replace("{CuffShirt}", model.CuffShirt);
                    //Coat
                    content = content.Replace("{QuantityCoat}", model.QuantityCoat.ToString());
                    content = content.Replace("{FitCoat}", model.FitCoat);
                    content = content.Replace("{ButtonCoat}", model.ButtonCoat);
                    content = content.Replace("{BreastCoat}", model.BreastCoat);
                    content = content.Replace("{LapelCoat}", model.LapelCoat);
                    //Material
                    content = content.Replace("{Material}", model.Material);
                    content = content.Replace("{NameMaterial}", model.NameMaterial);
                    content = content.Replace("{TotalLength}", model.TotalLength);
                    content = content.Replace("{LengthJacketShirt}", model.LengthJacketShirt);
                    content = content.Replace("{Shoulder}", model.Shoulder);
                    content = content.Replace("{NToS}", model.NToS);
                    content = content.Replace("{SleeveLength}", model.SleeveLength);
                    content = content.Replace("{SleeveWidth}", model.SleeveWidth);
                    content = content.Replace("{Armshole}", model.Armshole);
                    content = content.Replace("{Chest}", model.Chest);
                    content = content.Replace("{WaistJacketShirt}", model.WaistJacketShirt);
                    content = content.Replace("{HipJacketShirt}", model.HipJacketShirt);
                    content = content.Replace("{ChestWidth}", model.ChestWidth);
                    content = content.Replace("{AroundNeck}", model.AroundNeck);
                    content = content.Replace("{BackWidth}", model.BackWidth);
                    content = content.Replace("{LengthTrousers}", model.LengthTrousers);
                    content = content.Replace("{HipTrousers}", model.HipTrousers);
                    content = content.Replace("{Crotch}", model.Crotch);
                    content = content.Replace("{ThighWidth}", model.ThighWidth);
                    content = content.Replace("{CalfWidth}", model.CalfWidth);
                    content = content.Replace("{AnkleWidth}", model.AnkleWidth);
                    content = content.Replace("{WaistWidth}", model.WaistWidth);

                    content = content.Replace("{Request}", model.Request);

                    content = content.Replace("{HotelName}", hotel.Name);
                    content = content.Replace("{HotelEmail}", hotel.Email);
                    content = content.Replace("{HotelTel}", hotel.Tel);
                    content = content.Replace("{Website}", hotel.Website);
                    MailHelper.SendMail(model.Email, sendEmail.Title, content);
                    MailHelper.SendMail(hotel.Email, hotel.Name + " (" + model.Code + ") - Order of " + model.LastName, content);

                }
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Redirect("/Order/Messages/?status=" + status);
        }


        [HttpGet]
        public ActionResult Messages()
        {
            using (var db = new MyDbDataContext())
            {
                SendEmail sendEmail =
                    db.SendEmails.FirstOrDefault(
                        a => a.Type == TypeSendEmail.Order && a.LanguageID == Request.Cookies["LanguageID"].Value);

                string status = Request.Params["status"];
                ViewBag.Messages = "";
                if (string.IsNullOrEmpty(status) == false)
                {
                    if (status.Equals("success"))
                    {
                        ViewBag.Messages = sendEmail.Success;
                    }
                    else
                    {
                        ViewBag.Messages = sendEmail.Error;
                    }
                }
                else
                {
                    ViewBag.Messages = sendEmail.Error;
                }
                return View();
            }
        }
    }
}
