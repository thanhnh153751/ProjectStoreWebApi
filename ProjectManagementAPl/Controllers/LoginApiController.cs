using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementAPl.Models;
using ProjectManagementAPl.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManagementAPl.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private IConfiguration _config;
        private ICustomerRepository _repository;
        private IMapper _mapper;
        public LoginApiController(IConfiguration config, ICustomerRepository repository, IMapper mapper)
        {
            _config = config;
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return StatusCode(500, "Incorrect email and password!");
        }

        private string Generate(UserViewModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.customerId.ToString()),
                new Claim(ClaimTypes.Email, user.username),
                new Claim(ClaimTypes.Role, user.roleId.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserViewModel> Authenticate(UserLogin userLogin)
        {

            try
            {
                var currentUser = await _repository.AuthenticationCustomer(userLogin.email, userLogin.password);

                if (currentUser != null)
                {
                    var model = _mapper.Map<UserViewModel>(currentUser);
                    return model;
                }
                if (currentUser == null)
                {
                    string memberId = _config["DefaultAccount:MemberId"];
                    string email = _config["DefaultAccount:Email"];
                    string password = _config["DefaultAccount:Password"];
                    if (userLogin.email == email && userLogin.password == password)
                    {
                        UserViewModel member = new UserViewModel();
                        member.customerId = int.Parse(memberId);
                        member.username = email;
                        member.password = password;
                        member.roleId = 1;
                        return member;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "???");
            }

            return null;
        }
    }
}
