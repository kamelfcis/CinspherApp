using System;
using System.Collections.Generic;
using System.Text;

namespace CinspherApp.Models
{
    public partial class AccountRegister
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
