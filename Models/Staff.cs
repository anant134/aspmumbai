using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class Staff:Common
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int DesignationId { set; get; }

    }
}