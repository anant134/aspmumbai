using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class Common
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 1;
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}