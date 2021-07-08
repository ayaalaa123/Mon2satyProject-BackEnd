using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class SupplierPrivateTender
    {
        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        [Key, Column(Order = 0)]
        public int SupplierID { get; set; }

        [ForeignKey("PrivateTenderID")]
        public PrivateTender PrivateTender { get; set; }
        [Key, Column(Order = 1)]
        public int PrivateTenderID { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}