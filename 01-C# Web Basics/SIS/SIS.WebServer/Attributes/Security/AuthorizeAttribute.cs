using System;
using SIS.MvcFramework.Identity;

namespace SIS.MvcFramework.Attributes.Security
{
    public class AuthorizeAttribute : Attribute
    {
        private readonly string authority;

        public AuthorizeAttribute(string authority = "authorized")
        {
            this.authority = authority;
        }

        private bool IsLoggedIn(Principal principal)
        {
            return principal != null;
        }

        public bool IsInAuthority(Principal principal)
        {
            if (!IsLoggedIn(principal))
            {
                return authority == "anonymous";
            }

            return authority == "authorized" || principal.Roles.Contains(authority.ToLower());
        }
    }
}
