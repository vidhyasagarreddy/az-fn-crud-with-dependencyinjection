using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement
{
    public enum AccessTokenStatus
    {
        Valid,
        Expired,
        Error,
        NoToken
    }
}
