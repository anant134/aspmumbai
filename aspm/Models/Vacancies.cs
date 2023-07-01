using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class Vacancies : Common
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Oldname { get; set; }
        public DateTime Date { get; set; }
    }
}