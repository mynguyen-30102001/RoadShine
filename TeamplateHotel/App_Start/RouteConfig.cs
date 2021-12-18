using System.Web.Mvc;
using System.Web.Routing;

namespace TeamplateHotel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Reservation
            routes.MapRoute("ReservationB", "ReservationBook", new
            {
                controller = "ReservationBook",
                action = "Index",
            });

            //ReservationBooing
            routes.MapRoute("ReservationBooking", "ReservationBook/SendReservation", new
            {
                controller = "ReservationBook",
                action = "SendReservation",
            });
            //ReservationMessage

            routes.MapRoute("ReservationMessage", "ReservationBook/Messages", new
            {
                controller = "ReservationBook",
                action = "Messages",
            });
            routes.MapRoute("Tocontact", "tocontact", new
            {
                controller = "home",
                action = "ToContact",
            });
            //contact
            routes.MapRoute("contact", "contactt", new
            {
                controller = "home",
                action = "contact",
            });
            //sendcontact
            routes.MapRoute("SendContact", "send-contact", new
            {
                controller = "Home",
                action = "SendContact",
            });
            routes.MapRoute("SendMessage", "contact/Messages", new
            {
                controller = "Home",
                action = "Messages",
            });
            //All the menu
            routes.MapRoute("TheMenu", "expanded-menu", new
            {
                controller = "Home",
                action = "TheMenu",
            });
            //view page cart
            routes.MapRoute("ViewCart", "lstcart", new
            {
                controller = "Cart",
                action = "Index",
            });

            //Add To cart
            routes.MapRoute("AddProductTocart", "ThemVaoGio", new
            {
                controller = "Cart",
                action = "ThemVaoGio",
            });
            //get lst cart
            routes.MapRoute("ListCart", "Getlstcart", new
            {
                controller = "Cart",
                action = "DanhSach",
            });
            routes.MapRoute("Filter_Product", "filter-product", new
            {
                controller = "Home",
                action = "FilterProduct",
            });

            //search menu
            routes.MapRoute(
               name: "SearchMenu",
               url: "search-menu",
               defaults: new { controller = "Search", action = "SearchMenu" },
               namespaces: new[] { "TeamplateHotel.Controllers" }
            );
            //Edit cart
            routes.MapRoute("EditCart", "SuaSoLuong", new
            {
                controller = "Cart",
                action = "SuaSoLuong",
            });
            //Del cart
            routes.MapRoute("DelCart", "XoaKhoiGio", new
            {
                controller = "Cart",
                action = "XoaKhoiGio",
            });
            //Del session
            routes.MapRoute("DestroySession", "DestroySession", new
            {
                controller = "Home",
                action = "DestroySession",
            });

            //Trang thanh toán
            routes.MapRoute("CheckOut", "checkout", new
            {
                controller = "Order",
                action = "index",
            });
            //Mã khuyến mãi
            routes.MapRoute("Promotion", "CheckCode", new
            {
                controller = "Cart",
                action = "CheckCode",
            });
            //Gửi thanh toán
            routes.MapRoute("SendCheckOut", "checkout/sendcheckout", new
            {
                controller = "Order",
                action = "SendBooking",
            });
            //Thông báo khi đặt món xong

            routes.MapRoute("MessagesOrder", "Order/Messages", new
            {
                controller = "Order",
                action = "Messages",
            });

            //thông báo search
            routes.MapRoute("SearchMessage", "messageSearch", new
            {
                controller = "Search",
                action = "Messages",
            });

            routes.MapRoute("LoadMore", "article-loadmore", new
            {
                controller = "ArticleMore",
                action = "LoadMore",
            });


            routes.MapRoute("ViewMoreIndex", "ViewMore", new
            {
                controller = "LoadMore",
                action = "Index",
            });
            routes.MapRoute("ViewMore", "GetData", new
            {
                controller = "LoadMore",
                action = "GetData",
            });
            routes.MapRoute(
                name: "Error",
                url: "error-404",
                defaults: new { controller = "Error", action = "NotFound" }
            );
            routes.MapRoute("Default", "{aliasMenuSub}/{idSub}/{aliasSub}", new
            {
                controller = "Home",
                action = "Index",
                aliasMenuSub = UrlParameter.Optional,
                idSub = UrlParameter.Optional,
                aliasSub = UrlParameter.Optional
            }
                );
        }
    }
}