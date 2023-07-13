using AutoMapper;
using BusinessObjects.Entities;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPl.ViewModels;

namespace ProjectManagementAPl.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        //private IUserRepository repository;
        //private IMapper _mapper;

        //public MembersController(IUserRepository repository, IMapper mapper)
        //{
        //    this.repository = repository;
        //    _mapper = mapper;
        //}


        //[Authorize(Roles = "admin@estore.com")]
        //[HttpGet]
        //public ActionResult<List<MemberViewModel>> GetMembers()
        //{
        //    try
        //    {
        //        var list = repository.GetMembers();
        //        return _mapper.Map<List<MemberViewModel>>(list);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while retrieving the product.");
        //    }
        //}



        //[Authorize(Roles = "2")]
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserViewModel>> GetMember(int id)
        //{
        //    try
        //    {
        //        var member = repository.GetMemberById(id);

        //        if (member == null)
        //        {
        //            return NotFound();
        //        }

        //        return _mapper.Map<UserViewModel>(member);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, "An error occurred while retrieving products.");
        //    }
        //}


        //// PUT: api/Products/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "2")]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateMember(int id, UserViewModel p)
        //{
        //    try
        //    {
        //        var mep = repository.GetMemberById(id);
        //        if (mep == null)
        //            return NotFound();

        //        var member = _mapper.Map<User>(p);
        //        repository.UpdateMember(member);
        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while updating the product.");
        //    }
        //}

        //// POST: api/Products
        //[Authorize(Roles = "admin@estore.com")]
        //[HttpPost]
        //public async Task<ActionResult<MemberViewModel>> PostMember(MemberViewModel p)
        //{
        //    try
        //    {
        //        var member = _mapper.Map<Member>(p);
        //        repository.SaveMember(member);
        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while creating the product.");
        //    }
        //}

        //// DELETE: api/Products/5
        //[Authorize(Roles = "admin@estore.com")]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMember(int id)
        //{
        //    try
        //    {
        //        var p = repository.GetMemberById(id);
        //        if (p == null)
        //            return NotFound();
        //        repository.DeleteMember(p);
        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while deleting the product.");
        //    }
        //}



        //private bool ProductExists(int id)
        //{
        //    return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        //}
    }
}
