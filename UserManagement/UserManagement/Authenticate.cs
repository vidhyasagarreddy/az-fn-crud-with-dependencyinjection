using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UserManagement
{
    public class Authenticate
    {
        private readonly IAccessTokenProvider accessTokenProvider;

        public Authenticate(IAccessTokenProvider accessTokenProvider)
        {
            this.accessTokenProvider = accessTokenProvider;
        }

        [FunctionName("authenticate")]
        public Task<string> AuthenticateUser([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "authenticate")] HttpRequest req, ILogger log)
        {
            User user = new User
            {
                Email = "vidhya.sagar@technovert.com",
                Name = "Vidhya Sagar Reddy",
                Identifier = Guid.NewGuid().ToString()
            };

            return accessTokenProvider.CreateJWTAsync(user);
        }
    }
}
