using AutoMapper;
using BusinessObjects.Entities;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProjectManagementAPl.ViewModels;

namespace ProjectManagementAPl.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "1")]
    public class CategorysController : ODataController
    {
        private ICategoryRepository repository;
        private IMapper _mapper;

        public CategorysController(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetCategorys()
        {
            try
            {
                var task = repository.GetAllCategory();
                return Ok(await task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }


        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var task = await repository.FindCategoryById(id);
                var member = _mapper.Map<CategoryModelApi>(task);
                return Ok(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }


        //[Authorize(Roles = "admin@estore.com")]
        [HttpPost]        
        public async Task<ActionResult<BookViewModel>> postCategory([FromBody] ProductViewModel p)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return StatusCode(500, "An error occurred while creating.");
            }
            try
            {
                var member = _mapper.Map<Category>(p);
                await repository.Create(member);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating.");
            }
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> putCategory([FromODataUri] int key, [FromBody] ProductViewModel p)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var mep = await repository.FindCategoryById(key);
                if (mep == null)
                    return NotFound();

                var model = _mapper.Map<Category>(p);
                await repository.Update(model);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating.");
            }
        }

        //[Authorize(Roles = "admin@estore.com")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCategory([FromODataUri] int key)
        {
            try
            {
                var p = await repository.FindCategoryById(key);
                if (p == null)
                    return NotFound();
                await repository.Delete(p.First());
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting.");
            }
        }

    }
}
