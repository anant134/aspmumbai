using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspm.Models
{
	public class User:Common
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public int RoleId{ get; set; }
    }
}