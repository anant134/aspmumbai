using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class Designation:Common
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int SubCategoryId { get; set; }

        public int CategoryId { get; set; }

        //public virtual SubCategory SubCategorys { set; get; }
        //public virtual Category Categorys { set; get; }
    }
}