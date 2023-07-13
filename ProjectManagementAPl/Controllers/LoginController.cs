using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementAPl.Models;
using ProjectManagementAPl.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManagementAPl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private IConfiguration _config;
        //private IUserRepository _repository;
        //private IMapper _mapper;
        //public LoginController(IConfiguration config, IUserRepository repository, IMapper mapper)
        //{
        //    _config = config;
        //    _repository = repository;
        //    _mapper = mapper;
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Login([FromBody] UserLogin userLogin)
        //{
        //    var user = Authenticate(userLogin);

        //    if (user != null)
        //    {
        //        var token = Generate(user);
        //        return Ok(token);
        //    }

        //    return NotFound("User not found");
        //}

        //private string Generate(UserViewModel user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
        //        new Claim(ClaimTypes.Email, user.email_address),
        //        new Claim(ClaimTypes.Role, user.role_id.ToString())
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Audience"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(15),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private UserViewModel Authenticate(UserLogin userLogin)
        //{
        //    var currentUser = _repository.GetMemberByEmail(userLogin.Email, userLogin.Password);

        //    if (currentUser != null)
        //    {
        //        return _mapper.Map<UserViewModel>(currentUser);
        //    }
        //    if (currentUser == null)
        //    {
        //        string memberId = _config["DefaultAccount:MemberId"];
        //        string email = _config["DefaultAccount:Email"];
        //        string password = _config["DefaultAccount:Password"];
        //        if (userLogin.Email == email && userLogin.Password == password)
        //        {
        //            UserViewModel member = new UserViewModel();
        //            member.user_id = int.Parse(memberId);
        //            member.email_address = email;
        //            member.password = password;
        //            member.role_id = 1;
        //            return member;
        //        }
        //    }

        //    return null;
        //}
    }
}
