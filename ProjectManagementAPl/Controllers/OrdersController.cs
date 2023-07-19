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
    public class OrdersController : ODataController
    {
        private IOrderRepository repository;
        private IMapper _mapper;

        public OrdersController(IOrderRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        //[HttpGet]
        //[EnableQuery]
        //public async Task<IActionResult> GetCategorys()
        //{
        //    try
        //    {
        //        var task = repository.GetAllCategory();
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
        //public async Task<IActionResult> GetCategory(int id)
        //{
        //    try
        //    {
        //        var task = await repository.FindCategoryById(id);
        //        var member = _mapper.Map<CategoryModelApi>(task);
        //        return Ok(member);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return StatusCode(500, "An error occurred while retrieving the product.");
        //    }
        //}


        //[Authorize(Roles = "admin@estore.com")]
        [HttpPost]       
        public async Task<ActionResult> AddToCart([FromBody] CartItemModel cartItem)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return StatusCode(500, "An error occurred while creating.");
            }
            try
            {
                await repository.AddToCart(cartItem.productId, 1);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating.");
            }
        }

    }
}
