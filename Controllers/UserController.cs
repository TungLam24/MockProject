using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MockProject.Data;
using MockProject.Models;
using MockProject.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MockProject.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _db;
        private APIResponse _response;
        private readonly IMapper _mapper;
        private string _secretKey;
        public UserController(ApplicationDBContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _response = new APIResponse();
            _mapper = mapper;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Registration([FromBody] RegistrationRequest user)
        {
            try
            {
                if (await _db.LocalUser.FirstOrDefaultAsync(i => i.UserName == user.UserName) != null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Username existed");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _db.LocalUser.FirstOrDefaultAsync(i => i.Email == user.Email) != null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Email existed");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _db.LocalUser.FirstOrDefaultAsync(i => i.UserName == user.UserName) != null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("UserName existed");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                await _db.LocalUser.AddAsync(_mapper.Map<LocalUser>(user));
                await _db.SaveChangesAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = user;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequest user)
        {
            try
            {
                var account = await _db.LocalUser.FirstOrDefaultAsync(i => i.UserName == user.UserName && i.Password == user.PassWord);
                if (account == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode =HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Wrong UserName or Password");
                    return NotFound(_response);
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, account.Id.ToString()),
                        new Claim(ClaimTypes.Role, account.Role)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                LoginResponse res = new LoginResponse()
                {
                    User = account,
                    Token = tokenHandler.WriteToken(token)
                };
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = res;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
