using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class SupplierPhones
    {
        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        [Key, Column(Order = 0)]
        public int SupplierID { get; set; }


        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
    }
}