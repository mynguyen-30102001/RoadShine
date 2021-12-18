using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CheckNamespace
namespace ProjectLibrary.Database
// ReSharper restore CheckNamespace
{

    public partial class User
    {
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
    public partial class Article
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }

    public partial class Poject
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }
    public partial class Product
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }
    public partial class Customer
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }
    public partial class Order
    {
        [NotMapped]
        public List<ListItemCart> ListItemCarts { get; set; }
    }

    public class ListItemCart
    {
        public int PID { get; set; }
        public string PName { get; set; }
        public decimal? PPrice { get; set; }
        public int PQuantity { get; set; }
        public DateTime DateBook { get; set; }
        public string PRequest { get; set; }
        public int Index { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal Total { get; set; }
        public double Rate { get; set; }
        public string CodePromo { get; set; }

        public string PDesception { get; set; }
    }
}
