using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class SupplierPublicTender
    {

        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        [Key, Column(Order = 0)]
        public int SupplierID { get; set; }

        [ForeignKey("PublicTenderID")]
        public PublicTender PublicTender { get; set; }
        [Key, Column(Order = 1)]
        public int PublicTenderID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Applied { get; set; }

        public string Paid { get; set; }

        public SupplierPublicTender()
        {
            Applied = Paid = "NO";
        }
    }
}