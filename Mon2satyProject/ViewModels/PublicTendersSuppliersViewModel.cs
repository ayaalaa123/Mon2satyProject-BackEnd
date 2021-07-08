using Mon2satyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mon2satyProject.ViewModels
{
    public class PublicTendersSuppliersViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }

        public IEnumerable<PublicTender> PublicTenders { get; set; }
    }
}