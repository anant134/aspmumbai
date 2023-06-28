using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class Tcs: Common
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public int AdmNo { get; set; }
        public String Name { get; set; }
        public String Class { get; set; }
        public String ClassName { get; set; }
        public String FatherName { get; set; }

        public int TcNo { get; set; }
        public String SessionYear { get; set; }
        
    }
}