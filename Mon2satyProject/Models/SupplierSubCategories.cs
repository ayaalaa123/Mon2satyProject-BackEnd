using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class SupplierSubCategories
    {
        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        [Key, Column(Order = 0)]
        public int SupplierID { get; set; }


        [ForeignKey("SubCategoryID")]
        public SubCategory SubCategory { get; set; }
        [Key, Column(Order = 1)]
        public int SubCategoryID { get; set; }
    }
}