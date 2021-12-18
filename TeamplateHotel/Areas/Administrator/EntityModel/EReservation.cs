using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EReservation
    {

        public int ReservationId { get; set; }
        public string  Name { get; set; }

        public string CheckIn { get; set; }

        public string _Time { get; set; }

        public string Party { get; set; }

        public string Email { get; set; }
        public string  _Message { get; set; }
        public string Tel { get; set; }

        public int NumberPeople { get; set; }

        public string DateBook { get; set; }

        public string checkin { get; set; }

        public string timebook { get; set; }

        public  bool check { get; set; }
    }
}