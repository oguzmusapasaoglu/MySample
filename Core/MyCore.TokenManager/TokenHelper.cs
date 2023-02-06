using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyCore.Common.ConfigHelper;
using MyCore.LogManager.ExceptionHandling;

namespace MyCore.Common.Token
{
    public class TokenHelper
    {
        public static string GenerateToken(string userId, string userName, string userPages)
        {
            var jwtConfig = ConfigurationHelper.GetConfig<JwtConfigModel>(JwtConfigModel.SectionName);
            try
            {
                var expireDate = new DateTimeOffset(DateTime.Now.AddHours(5)).DateTime;
                var issuer = jwtConfig.Issuer;
                var audience = jwtConfig.Audience;
                var encryptionKey = Encoding.ASCII.GetBytes(jwtConfig.Key);

                var signinCredential = new SigningCredentials(
                    new SymmetricSecurityKey(encryptionKey),
                    SecurityAlgorithms.HmacSha256);

                var userClaims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, userPages),
                    new Claim(JwtRegisteredClaimNames.Exp, expireDate.ToString())
                };
                var jwToken = new JwtSecurityToken(issuer: issuer,
                                               audience: audience,
                                               claims: userClaims,
                                               notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                                               expires: expireDate,
                                               signingCredentials: signinCredential
            );

                return new JwtSecurityTokenHandler().WriteToken(jwToken);
            }
            catch (Exception ex)
            {
                throw new KnownException(ExceptionTypeEnum.Fattal, ex);
            }
        }
        public static List<Claim> GetClaimsFromToken(string token)
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                return jwt.Claims.ToList();
            }
            catch (Exception ex)
            {
                throw new KnownException(ExceptionTypeEnum.Fattal, ex);
            }
        }
        public static int GetUserIdFromToken(string token)
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string userId = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value;
                return userId.ToInt();
            }
            catch (Exception ex)
            {
                throw new KnownException(ExceptionTypeEnum.Fattal, ex);
            }
        }

        public static string GetUserNameFromToken(string token)
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string userId = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
                return userId;
            }
            catch (Exception ex)
            {
                throw new KnownException(ExceptionTypeEnum.Fattal, ex);
            }
        }
    }
}
