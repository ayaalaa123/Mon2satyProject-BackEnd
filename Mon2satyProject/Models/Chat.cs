using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mon2satyProject.Models
{
    public class Chat
    {
        public int ID { get; set; }

        public string MessageFromSupplier { get; set; }


        public DateTime TimeFromSupplier { get; set; }

        public string MessageFromAdmin { get; set; }

        public DateTime TimeFromAdmin { get; set; }
    }
}