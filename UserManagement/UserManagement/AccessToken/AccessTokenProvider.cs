using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace UserManagement
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly string issuerToken;
        private readonly string audience;
        private readonly string issuer;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public AccessTokenProvider(string issuerToken, string audience, string issuer)
        {
            this.issuerToken = issuerToken;
            this.audience = audience;
            this.issuer = issuer;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public AccessTokenResult ValidateToken(HttpRequest request)
        {
            try
            {
                string token = request.GetBearerToken();

                // Grab token from header
                if (token.IsNotNullOrEmpty())
                {
                    var tokenParams = new TokenValidationParameters()
                    {
                        RequireSignedTokens = true,
                        ValidAudience = audience,
                        ValidateAudience = true,
                        ValidIssuer = issuer,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = GetSymmetricSecurityKey()
                    };

                    // Validate token
                    var result = jwtSecurityTokenHandler.ValidateToken(token, tokenParams, out var securityToken);
                    return AccessTokenResult.Success(result);
                }
                else
                {
                    return AccessTokenResult.NoToken();
                }
            }
            catch (SecurityTokenExpiredException)
            {
                return AccessTokenResult.Expired();
            }
            catch (Exception ex)
            {
                return AccessTokenResult.Error(ex);
            }
        }

        public async Task<string> CreateJWTAsync(User user, int daysValid = 7)
        {
            var claims = CreateClaims(user);

            // Create JWToken
            var token = jwtSecurityTokenHandler.CreateJwtSecurityToken(issuer: issuer,
                audience: audience,
                subject: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(daysValid),
                signingCredentials:
                new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature));

            return await Task.FromResult(jwtSecurityTokenHandler.WriteToken(token));
        }

        private ClaimsIdentity CreateClaims(User user)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Identifier));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));

            return claimsIdentity;
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.Default.GetBytes(issuerToken));
        }
    }
}
