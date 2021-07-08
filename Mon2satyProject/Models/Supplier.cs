using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Susspended = "NO";
        }

        public Supplier(Supplier supplier)
        {
            CompanyName = supplier.CompanyName;
            Address = supplier.Address;
            AddedDate = DateTime.Now;
            Email = supplier.Email;
            LegalPaperwork = supplier.LegalPaperwork;
            InfoPaperwork = supplier.InfoPaperwork;
            Password = supplier.Password;
            CompanyCode = supplier.CompanyCode;
            ManagerName = supplier.ManagerName;
            ManagerPhone = supplier.ManagerPhone;
        }

        public int ID { get; set; }
        
        [Required]
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
        
        [Required]
        [Display(Name = "Startup Dealing Date")]
        public DateTime AddedDate { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }
        
        [Required]
        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }
        
        [Required]
        [Display(Name = "Manager Phone")]
        public string ManagerPhone { get; set; }

        public string Susspended { get; set; }
        //public ICollection<SupplierPhones> SupplierPhones { get; set; }
        //public ICollection<SupplierFaxes> SupplierFaxes { get; set; }
        //public ICollection<SubCategory> SubCategories { get; set; }
    }
}