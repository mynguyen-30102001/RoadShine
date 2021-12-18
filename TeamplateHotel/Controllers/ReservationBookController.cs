using ProjectLibrary.Config;
using ProjectLibrary.Database;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace TeamplateHotel.Controllers
{
    public class ReservationBookController : Controller
    {
        //
        // GET: /ReservationBook/

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendReservation(Reservation model)
        {
            //model.CreateDate = DateTime.Now;

            //DateTime dt = DateTime.Parse("YYYY//MM/DD hh:mm tt");

            using (var db = new MyDbDataContext())
            {
                string status = "success";

                 Company company = CommentController.DetailCompany(Request.Cookies["LanguageID"].Value);

                try
                {
                     model.Seen = false;
                    model.CreateDate = DateTime.Now;
                    db.Reservations.InsertOnSubmit(model);
                    db.SubmitChanges();

                    SendEmail sendEmail =
                        db.SendEmails.FirstOrDefault(
                            a => a.Type == TypeSendEmail.Reservation && a.LanguageID == Request.Cookies["LanguageID"].Value);

                    sendEmail.Title = sendEmail.Title;
                    string content = sendEmail.Content;
                    //var abc = model._Time.ToString("hh:mm tt");
                    content = content.Replace("{Name}", model.Name);
                    content = content.Replace("{CheckIn}", model.CheckIn.ToString("MM/dd/yyyy"));
                    content = content.Replace("{TimeB}", model.TimeB.ToString());
                     content = content.Replace("{Email}", model.Email);
                    content = content.Replace("{Tel}", model.Tel);
                    content = content.Replace("{NumberPeople}", model.NumberPeople.ToString());
                    content = content.Replace("{MessageB}", model.MessageB);

                    MailHelper.SendMail(model.Email, sendEmail.Title, content);
                    MailHelper.SendMail(company.Email, sendEmail.Title + model.Name, content);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    status = "error";
                }
                return Redirect("/ReservationBook/Messages/?status=" + status);

            }

        }
        [HttpGet]
        public ActionResult Messages()
        {
            using (var db = new MyDbDataContext())
            {
                SendEmail sendEmail =
                    db.SendEmails.FirstOrDefault(
                        a => a.Type == TypeSendEmail.Reservation && a.LanguageID == Request.Cookies["LanguageID"].Value);

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
