using Mon2satyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mon2satyProject.ViewModels
{
    public class SupplierTelFaxViewModel
    {
        public Supplier Supplier { get; set; }

        public string SupplierPhone { get; set; }

        public string SupplierFax { get; set; }

        public IEnumerable<SubCategory> subCategories { get; set; }
    }
}