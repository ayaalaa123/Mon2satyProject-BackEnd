using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class PrivateTender
    {
        public int ID { get; set; }

        public string Brochure { get; set; }

        public DateTime Date { get; set; }



        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        public int? SupplierID { get; set; }


        [ForeignKey("SubCategoryID")]
        public SubCategory SubCategory { get; set; }
        [Required]
        public int SubCategoryID { get; set; }
        [Required]
        public DateTime expireDate { get; set; }
    }
}