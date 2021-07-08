﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class SubCategory
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }


        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        
        //public ICollection<Supplier> Suppliers { get; set; }
    }
}