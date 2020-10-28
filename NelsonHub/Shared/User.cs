using System;
using System.Collections.Generic;
using System.Text;

namespace NelsonHub.Shared
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

        public string Role { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}
