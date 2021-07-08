using Mon2satyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mon2satyProject.ViewModels
{
    public class CategoriesSubCategoriesSuppliers
    {

        //public Category Category { get; set; }

        //public IEnumerable<SubCategory> SubCategories { get; set; }

        //public IEnumerable<SupplierSubCategories> SupplierSubCategories { get; set; }

        public Object Category { get; set; }

        public IEnumerable<Object> SubCats { get; set; }

        public IEnumerable<Object> Suppliers { get; set; }

    }
}