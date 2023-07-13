using AutoMapper;
using BusinessObjects.Entities;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProjectManagementAPl.Models;
using ProjectManagementAPl.ViewModels;

namespace ProjectManagementAPl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin@estore.com")]
    public class RegiterController : ControllerBase
    {
        //private IUserRepository repository;
        //private IMapper _mapper;

        //public RegiterController(IUserRepository repository, IMapper mapper)
        //{
        //    this.repository = repository;
        //    _mapper = mapper;
        //}       

        //[HttpPost]
        //public IActionResult Regiter([FromBody] UserRegiter p)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //        //return StatusCode(500, "An error occurred while creating the product.");
        //    }
        //    if (!p.Password.Equals(p.PasswordConfirm))
        //    {
        //        return BadRequest("Password Confirm not correct!");
        //    }
        //    try
        //    {
        //        var member = _mapper.Map<UserViewModel>(p);
        //        member.role_id = 2;
        //        member.pub_id = null;
        //        var memberAdd = _mapper.Map<User>(member);
        //         repository.SaveMember(memberAdd);
        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while creating the account.");
        //    }
        //}
       

    }
}
