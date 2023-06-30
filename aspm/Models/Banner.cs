using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class Banner: Common
    {
       
        public int Id { get; set; }
        public String NewFilename { get; set; }
        public String OldFilename { get; set; }
        public String Path { get; set; }

        public int SortId { get; set; }


    }
}