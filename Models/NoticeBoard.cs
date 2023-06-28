using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class NoticeBoard:Common
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Oldname { get; set; }

       
    }
}