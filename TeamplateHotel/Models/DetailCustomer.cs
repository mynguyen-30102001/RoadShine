using System.Collections.Generic;
using ProjectLibrary.Database;

namespace TeamplateHotel.Models
{
    public class DetailCustomer
    {
        public Customer Customer { get; set; }
        public List<Customer> Customers { get; set; }
    }
}