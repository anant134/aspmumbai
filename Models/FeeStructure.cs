using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class FeeStructure:Common
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int SubCategoryId { get; set; }

        public int CategoryId { get; set; }

        public decimal Offrs { get; set; }
        public decimal JCOs { get; set; }
        public decimal OR { get; set; }
        public decimal Civil { get; set; }



    }
}