using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement
{
    public class UnauthorizedUserException: Exception
    {
        public UnauthorizedUserException(string message = "Unauthorized..!!!") : base(message)
        {
        }
    }
}
