using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class SubCategory:Common
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { set; get; }

        public bool isGroup { get; set; } = false;
    }
}