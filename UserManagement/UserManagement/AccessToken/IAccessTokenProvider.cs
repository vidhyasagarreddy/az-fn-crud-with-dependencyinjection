using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement
{
    public interface IAccessTokenProvider
    {
        AccessTokenResult ValidateToken(HttpRequest request);

        Task<string> CreateJWTAsync(User user, int daysValid = 7);
    }
}
