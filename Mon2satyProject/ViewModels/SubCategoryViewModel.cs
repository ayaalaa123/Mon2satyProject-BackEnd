using Mon2satyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mon2satyProject.ViewModels
{
    public class SubCategoryViewModel
    {
        public SubCategory SubCategory { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        
    }
}