using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class Login
    {
        public Login()
        {
            Susspended = "NO";
        }
        [Key]
        public string CompanyCode { get; set; }

        [Required]
        public string Password { get; set; }

        [DefaultValue("NO")]
        public string Susspended { get; set; }
    }
}