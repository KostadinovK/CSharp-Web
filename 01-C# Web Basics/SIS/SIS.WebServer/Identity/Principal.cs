using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MvcFramework.Identity
{
    public class Principal
    {
        public Principal()
        {
            Roles = new List<string>();
        }

        public  string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
