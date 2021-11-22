
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tasks.Common;
using Tasks.Data;

namespace Tasks.Authorization.Token
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        private IEnumerable<Claim> GetTokenClaims(ApplicationUser user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim("role", _userManager.GetRolesAsync(user).Result.FirstOrDefault())
            };
        }

        public async Task SetRefreshToken(long userProfileId)
        {
            //Need Person/UserProfile table and reference for finishing query
            var user = _userManager.Users.Where(u => u.ProfileId == userProfileId).FirstOrDefault();
            var refreshToken = new JwtSecurityToken(
                    claims: GetTokenClaims(user),
                    expires: DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration.GetSection("RefreshToken:Expires").Value)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("RefreshToken:SecretKey").Value)), SecurityAlgorithms.HmacSha256)
                );
            var refreshResponseData = new
            {
                jwtType = "Bearer",
                token = new JwtSecurityTokenHandler().WriteToken(refreshToken),
                validTo = refreshToken.ValidTo
            };

            var refreshTokenSetResult = await _userManager.SetAuthenticationTokenAsync(user, "FindLoyalty", "RefreshToken", refreshResponseData.token);
            if (!refreshTokenSetResult.Succeeded)
            {
                //Log.Information($"TokenService.SetRefreshToken(UserId: {user.Id})");
                foreach (var item in refreshTokenSetResult.Errors)
                {
                  //  Log.Error(item.ToString());
                }
            }
        }

        public async Task<IResult<TokenModel>> GenerateTokens(ApplicationUser user)
        {
            var response = new Result<TokenModel>();
            try
            {
                var userClaims = await _userManager.GetClaimsAsync(user);

                var securityToken = new JwtSecurityToken(
                        issuer: _configuration.GetSection("JWT:Issuer").Value,
                        audience: _configuration.GetSection("JWT:Audience").Value,
                        claims: GetTokenClaims(user).Union(userClaims),
                        expires: DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration.GetSection("JWT:Expires").Value)),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SecretKey").Value)), SecurityAlgorithms.HmacSha256)
                    );

                var securityAccessToken = new SecurityAccessToken()
                {
                    JwtType = "Bearer",
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    ValidTo = securityToken.ValidTo
                };

                var refreshToken = new JwtSecurityToken(
                        claims: GetTokenClaims(user),
                        expires: DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration.GetSection("RefreshToken:Expires").Value)),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("RefreshToken:SecretKey").Value)), SecurityAlgorithms.HmacSha256)
                    );
                var refreshAccessToken = new RefreshAccessToken()
                {
                    JwtType = "Bearer",
                    Token = new JwtSecurityTokenHandler().WriteToken(refreshToken),
                    ValidTo = refreshToken.ValidTo
                };

                var tokens = new TokenModel()
                {
                    SecurityToken = securityAccessToken,
                    RefreshToken = refreshAccessToken,
                };
                response.Value = tokens;
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                //Log.Information($"TokenService.GenerateTokens(PersonId: {user.Id})");
                //Log.Error(ex.GetBaseException().Message);
            }
            return response;
        }

        public IResult<ClaimsPrincipal> GetTokenPrincipal(string token)
        {
            var result = new Result<ClaimsPrincipal>();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("RefreshToken:SecretKey").Value)),
                    ValidateIssuerSigningKey = true
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                result.Value = principal;
                result.Succeeded = true;

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                //Log.Information($"TokenService.GetTokenPrincipal(Token: {token})");
                //Log.Error(ex.GetBaseException().Message);
            }

            return result;
        }
    }
}
