using AutoMapper;
using BusinessObjects.Entities;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProjectManagementAPl.ViewModels;

namespace ProjectManagementAPl.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "admin@estore.com")]
    public class AuthorsController : ODataController
    {
        //private IAuthorRepository repository;
        //private IMapper _mapper;

        //public AuthorsController(IAuthorRepository repository, IMapper mapper)
        //{
        //    this.repository = repository;
        //    _mapper = mapper;
        //}


        //[HttpGet]
        //[EnableQuery]
        //public async Task<IActionResult> GetAuthors()
        //{
        //    try
        //    {
        //        var task = repository.GetAllAuthors(); // Assuming GetAllBooks() returns Task<IQueryable<Book>>
        //        return Ok(await task);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return StatusCode(500, "An error occurred while retrieving the product.");
        //    }
        //}


        //[EnableQuery]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAuthor([FromODataUri] int key)
        //{
        //    try
        //    {
        //        var task = repository.FindAuthorById(key);

        //        return Ok(await task);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return StatusCode(500, "An error occurred while retrieving the product.");
        //    }
        //}


        ////[Authorize(Roles = "admin@estore.com")]
        //[HttpPost]
        //public async Task<ActionResult<BookViewModel>> PostAuthor([FromBody] AuthorViewModel p)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        //return BadRequest(ModelState);
        //        return StatusCode(500, "An error occurred while creating the product.");
        //    }
        //    try
        //    {
        //        var member = _mapper.Map<Author>(p);
        //        await repository.Create(member);
        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while creating the product.");
        //    }
        //}

        ////[Authorize]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAuthor([FromODataUri] int key, [FromBody] AuthorViewModel p)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var mep = await repository.FindAuthorById(key);
        //        if (mep == null)
        //            return NotFound();

        //        var book = _mapper.Map<Author>(p);
        //        await repository.Update(book);
        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while updating the product.");
        //    }
        //}

        ////[Authorize(Roles = "admin@estore.com")]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAuthor([FromODataUri] int key)
        //{
        //    try
        //    {
        //        var p = await repository.FindAuthorById(key);
        //        if (p == null)
        //            return NotFound();
        //        await repository.Delete(p.First());
        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while deleting the product.");
        //    }
        //}

    }
}
