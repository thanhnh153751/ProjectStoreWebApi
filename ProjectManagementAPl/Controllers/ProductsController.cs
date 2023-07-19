using AutoMapper;
using BusinessObjects.Entities;
using DataAccess;
using DataAccess.ModelViewOdata;
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
    public class ProductsController : ODataController
    {
        private IProductRepository repository;
        private IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var task = repository.GetAllProductest();
                return Ok(await task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByView()
        {
            try
            {
                var task = repository.ProductTopByView();

                return Ok(_mapper.Map<List<ProductModel>>(task));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts([FromODataUri] int key)
        {
            try
            {
                var task = repository.FindProductByIdOdata(key);

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
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var p = await repository.FindProductById(id);
                var member = _mapper.Map<ProductModelApi>(p);
                return Ok(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByCategoryId(int id)
        {
            try
            {
                var p = await repository.FindProductByCategoryId(id);
                var member = _mapper.Map<List<ProductModelApi>>(p);
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
        public async Task<ActionResult<ProductModelApi>> postProduct([FromBody] ProductModelApi p)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return StatusCode(500, "An error occurred while creating the product.");
            }
            p.status = true;
            try
            {
                var member = _mapper.Map<Product>(p);
                await repository.Create(member);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> putProduct([FromODataUri] int id, [FromBody] ProductModelApi p)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var mep = await repository.FindProductById(id);
                if (mep == null)
                    return NotFound();

                var book = _mapper.Map<Product>(p);
                if (string.IsNullOrEmpty(p.image))
                {
                    book.image = mep.image;
                }
                book.status = mep.status;
                await repository.Update(book);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        //[Authorize(Roles = "admin@estore.com")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteProduct([FromODataUri] int id)
        {
            try
            {
                var p = await repository.FindProductById(id);
                if (p == null)
                    return NotFound();
                await repository.Delete(p);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }

    }
}
