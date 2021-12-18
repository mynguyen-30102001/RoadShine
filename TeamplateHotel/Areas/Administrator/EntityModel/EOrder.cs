using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EOrder
    {

        public int ID { get; set; }
        public string Code { get; set; }

        public string FullName { get; set; }
 
        public string Email { get; set; }
        public string Request { get; set; }
        public int Phone { get; set; }

        public string Address { get; set; }

        public string DateBook { get; set; }

   
 

        public bool check { get; set; }
    }
}