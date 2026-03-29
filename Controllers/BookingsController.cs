using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Travel_Agency_2nd_Semester_Project.Models;
using System.Net.Http.Headers;
using System.Xml.Serialization;

namespace Travel_Agency_2nd_Semester_Project.Controllers
{
    public class BookingsController : Controller
    {
        private BrazsToursTravelAgencyDBEntitiesConString db = new BrazsToursTravelAgencyDBEntitiesConString();

        // GET: Bookings
        //[Route("BookingList")]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.AgeGroup).Include(b => b.Gender).Include(b => b.PaymentMethod).Include(b => b.Tour);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        [Route ("BookingDetails/{id:int?}")]
        public ActionResult Details(int? id)
        {
            if (id <=0 )
            {
                throw new Exception("Sorry, Invalid Booking ID");
            }
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                throw new HttpException(400, "Bad Request - Braz's Tours 400 error");
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                throw new HttpException(404, "Bad Request - Braz's Tours 404 error");
                //return HttpNotFound();
            }


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri ("http://localhost:55932/api/");

            //var httpResponseMessage = client.GetAsync("GetTourTips/" + booking.Tour);
            //var tourName = System.Web.HttpUtility.UrlEncode(booking.Tour.TourName);
            var tourName = Uri.EscapeDataString(booking.Tour.TourName.Replace("’", "'"));
            var httpResponseMessage = client.GetAsync("GetTourTips/" + tourName);
            httpResponseMessage.Wait();

            var responseMessageFromApi = httpResponseMessage.Result;

            if (responseMessageFromApi.IsSuccessStatusCode) 
            {
                var taskObjectRepresentingString = responseMessageFromApi.Content.ReadAsAsync<string>();
                taskObjectRepresentingString.Wait();

                ViewBag.InfoFromApi = taskObjectRepresentingString.Result;
            }
            else
            {
                ViewBag.InfoFromApi = "Error";
                ModelState.AddModelError(string.Empty, "No API Available");
            }


            using (var client2 = new HttpClient())
            {
                client2.BaseAddress = new Uri("http://localhost:55454/api/");

                var tourName2 = Uri.EscapeDataString(booking.Tour.TourName.Replace("’", "'"));
                var dateStr = booking.TourDate.ToString("yyyy-MM-dd");

                HttpResponseMessage response2 = client2.GetAsync($"GetWeatherForecast/{tourName2}/{dateStr}").Result;

                if (response2.IsSuccessStatusCode)
                {
                    dynamic dto = response2.Content.ReadAsAsync<dynamic>().Result;

                    string condition = dto.Condition ?? "";
                    string temperature = dto.Temperature ?? "";
                    string dateStrFromApi = dto.Date ?? "";

                    DateTime forecastDt;
                    bool hasDate = DateTime.TryParseExact(dateStrFromApi, "dd-MM-yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out forecastDt);

                    if (string.IsNullOrWhiteSpace(condition) && !hasDate)
                    {
                        
                        ViewBag.InfoFromApi2 = "No weather forecast available for this date.";
                    }
                    else
                    {
                        ViewBag.InfoFromApi2 = $"{condition}, {temperature}";
                    }
                }
                else
                {
                    ViewBag.InfoFromApi2 = "No weather forecast available for this date.";
                }
            }

            return View(booking);
        }

        // GET: Bookings/Create
       //[Route("NewBooking")]
        public ActionResult Create()
        {
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "AgeGroupDescription");
            ViewBag.Genders = db.Genders.ToList();
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "PaymentDescription");
            ViewBag.Tours = db.Tours.ToList();
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,FullName,Email,PhoneNumber,GenderID,AgeGroupID,TourID,NumberOfParticipants,TourDate,PaymentMethodID,SpecialComments,TermsAccepted,TotalPrice")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "AgeGroupDescription", booking.AgeGroupID);
            ViewBag.Genders = db.Genders.ToList();
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "PaymentDescription", booking.PaymentMethodID);
            ViewBag.Tours = db.Tours.ToList();
            return View(booking);
        }

        // GET: Bookings/Edit/5
        //[Route("EditBooking")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "AgeGroupDescription", booking.AgeGroupID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "GenderDescription", booking.GenderID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "PaymentDescription", booking.PaymentMethodID);
            //ViewBag.TourID = new SelectList(db.Tours, "TourID", "TourName", booking.TourID);
            ViewBag.Tours = db.Tours.ToList();
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,FullName,Email,PhoneNumber,GenderID,AgeGroupID,TourID,NumberOfParticipants,TourDate,PaymentMethodID,SpecialComments,TermsAccepted,TotalPrice")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "AgeGroupDescription", booking.AgeGroupID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "GenderDescription", booking.GenderID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "PaymentDescription", booking.PaymentMethodID);
            //ViewBag.TourID = new SelectList(db.Tours, "TourID", "TourName", booking.TourID);
            ViewBag.Tours = db.Tours.ToList();
            return View(booking);
        }

        // GET: Bookings/Delete/5
        //[Route("DeleteBooking")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
