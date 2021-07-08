using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class NewSupplier
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Compnay Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Company Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Legal Paperwork")]
        public string LegalPaperwork { get; set; }

        [Required]
        [Display(Name = "Information Paperwork")]
        public string InfoPaperwork { get; set; }

        [Display(Name = "Startup Dealing Date")]
        public string AddedDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }


        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }

        [Required]
        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }

        [Required]
        [Display(Name = "Manager Phone")]
        public string ManagerPhone { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Required]
        [Display(Name = "Sub Category")]
        public int SubCategoryID { get; set; }
    }
}