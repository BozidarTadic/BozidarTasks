
using System.Security.Claims;
using System.Threading.Tasks;
using Tasks.Common;
using Tasks.Data;

namespace Tasks.Authorization.Token
{
    public interface ITokenService
    {
        Task SetRefreshToken(long personId);
        Task<IResult<TokenModel>> GenerateTokens(ApplicationUser user);
        IResult<ClaimsPrincipal> GetTokenPrincipal(string token);
    }
}
