using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(UserManagement.Startup))]
namespace UserManagement
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(s =>
            {
                return new UserService();
            });

            string issuerToken = Environment.GetEnvironmentVariable("JwtAuthentication:IssuerToken") ?? "c5zTYSvg7U6DmRQkxaEY5ZHrkD6WCZPFJ6zqCj5LNMUsYpUbjpwVmx49atVtHXKhHWxTwXgBE2mKeCzegLY9jEJDGzneMsvfRCBLgqwVvD2CwwMFJfwhXnNEVPhXgMsSz4CXVXseaCbxk9AHJKrbMeXWWPevHb2nGwbJezz6qUzrJA9BMZ2eKfaThX9SAvDwvrJMjsVRQn7t6WtNn5VfmtXtvzT8rkUvQh5U4JCayU39nDtWpXYtJTmk3sNBQVJ4";
            string audience = Environment.GetEnvironmentVariable("JwtAuthentication:Audience") ?? "https://github.com";
            string issuer = Environment.GetEnvironmentVariable("JwtAuthentication:Issuer") ?? "https://github.com";
            builder.Services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>(s => new AccessTokenProvider(issuerToken, audience, issuer));
        }
    }
}
