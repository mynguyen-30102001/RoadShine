using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectLibrary.Database;

namespace TeamplateHotel.Models
{
    public class DetaiProduct
    {
        public Product Product { get; set; }
        public Product ProductLast { get; set; }
        public Product ProductNext { get; set; }
        public List<Product> Products { get; set; }

        public List<Product> ProductsPost { get; set; }

        public CartItem CartItem { get; set; }
        
        public List<ProductGallery> ProductGalleries { get; set; }
    }
}