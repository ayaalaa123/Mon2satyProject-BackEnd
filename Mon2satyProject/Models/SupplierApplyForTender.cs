using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class SupplierApplyForTender
    {
        public int ID { get; set; }

        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }

        public int? SupplierID { get; set; }

        [ForeignKey("TenderID")]
        public PublicTender PublicTender { get; set; }

        public int? TenderID { get; set; }
    }
}