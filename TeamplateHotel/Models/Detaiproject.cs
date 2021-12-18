using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using ProjectLibrary.Database;

namespace TeamplateHotel.Models
{
    public class Detaiproject
    { 
            public Poject Poject { get; set; }
            public List<Poject> Pojects { get; set; }
            public List<PojectGallery> PojectGalleries { get; set; }
        
    }
}