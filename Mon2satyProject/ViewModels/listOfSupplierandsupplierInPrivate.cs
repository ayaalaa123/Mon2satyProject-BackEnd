using Mon2satyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mon2satyProject.ViewModels
{
    public class listOfSupplierandsupplierInPrivate
    {
        public IEnumerable<SupplierPrivateTender> suppliers { get; set; }
        public Supplier supplier { get; set; }

    }
}