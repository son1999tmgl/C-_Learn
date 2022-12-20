using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Controllers.Data;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DTMyDbContext _context;
        private readonly AppSetting _appSettings;

        public UserController(DTMyDbContext context, IOptionsMonitor<AppSetting> 
            optionMonitor)
        {
            _appSettings = optionMonitor.CurrentValue;
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Validate(LoginModel model)
        {
            var user = _context.NguoiDungs.SingleOrDefault(p => p.UserName==model.Password
            && p.Password == model.Password);
            if(user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Mk hoặc Email ko đúng"
                });
            }

            var token = await GenerateToken(user);
            //cấp token
            return Ok(new ApiResponse
            {
                Success=true,
                Message = "Dn thành công",
                Data = token
            });
        }

        private async Task<TokenModel> GenerateToken(DTNguoiDung nguoiDung)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, nguoiDung.HoTen),
                      new Claim(JwtRegisteredClaimNames.Email, nguoiDung.Email),
                      new Claim(JwtRegisteredClaimNames.Sub, nguoiDung.Email),
                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                      new Claim("UserName", nguoiDung.UserName),
                      new Claim("Id", nguoiDung.Id.ToString()),
                      new Claim("TokenId", Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddSeconds(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken =  jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Luu database
            var refreshTokenEntity = new DTRefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                ExpireAt = DateTime.UtcNow.AddHours(1),
            };
            await _context.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };


        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenParam = new TokenValidationParameters
            {
                //Tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false, //ko kiểm tra về hết hạn
            };
            try
            {
                //Access cos valid format? 
                var tokenInVertification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenParam, out var validatedToken);
            
            
                //check thuat toan
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256, 
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Invalid token"
                        });
                    }

                }


                //check accessToken expire?

                var utcExpireDate = long.Parse(tokenInVertification.Claims.FirstOrDefault(
                    s =>s.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);

                if(expireDate > DateTime.UtcNow)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Access token has not yet expired"
                    });
                }


                //check refreshToken exits in DB
                var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token==model.RefreshToken);
                if(storedToken == null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token does not exits"
                    });
                }

                //check refreshToken is Uses is used/revoked
                if (storedToken.IsRevoked)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token has been revoked"
                    });
                }

                //accessToken id == jwtId in refredhToken?

                var jti = tokenInVertification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if(storedToken.JwtId != jti)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Token doesn'n match"
                    });
                }


                //update token is used 
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                await _context.SaveChangesAsync();


                //create new token
                var user = await _context.NguoiDungs.SingleOrDefaultAsync(ng => ng.Id == storedToken.UserId);
                var token = await GenerateToken(user);
                //cấp token
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Renew Token success",
                    Data = token
                });

                return Ok(new ApiResponse
                {
                    Success = true,
                });



            }
            catch(Exception ex)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid token"
                });
            }
        }


        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeIterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return dateTimeIterval;
        }
    }
}
