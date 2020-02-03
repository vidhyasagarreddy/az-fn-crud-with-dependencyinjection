using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UserManagement
{
    public class UserManagerFunction
    {
        private readonly UserService userService;
        public UserManagerFunction(UserService userService)
        {
            this.userService = userService;
        }

        [FunctionName("ListUsers")]
        public List<User> ListUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req,
            ILogger log)
        {
            return userService.GetAll();
        }

        [FunctionName("CreateUser")]
        public bool CreateUser(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] User user,
           ILogger log)
        {
            return userService.Add(user);
        }


        [FunctionName("DeleteUser")]
        public bool DeleteUser(
           [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "user/{email}")] HttpRequest req,
           ILogger log, string email)
        {
            return userService.Delete(email);
        }
    }
}
