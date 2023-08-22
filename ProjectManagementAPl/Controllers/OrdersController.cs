using AutoMapper;
using BusinessObjects.Entities;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProjectManagementAPl.ViewModels;
using System.Threading.Tasks;

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


        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCartItems(int id)
        {
            try
            {
                var task = await repository.GetCartByCustomerId(id);
                var member = _mapper.Map<List<AllCartItemModel>>(task);
                return Ok(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SentRequestOrder(int id)
        {
            try
            {
                await repository.SentRequestOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderByOrderId(int id)
        {
            try
            {
                var result = await repository.DeleteOrderByOrderId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ApprovalRequestOrder(int id)
        {
            try
            {
                var result = await repository.ApprovalRequestOrderByOrderId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllHistoryOrderByCumtomerId(int id)
        {
            try
            {
                var list = await repository.GetAllHistoryOrderByCumtomerId(id);
                var member = _mapper.Map<List<ListOrderCustomerByIdModel>>(list);
                return Ok(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHistoryOrderApprpved()
        {
            try
            {
                var list = await repository.GetAllHistoryOrderApprpved();
                var member = _mapper.Map<List<ListOrderCustomerByAdminModel>>(list);
                return Ok(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHistoryOrderPending()
        {
            try
            {
                var list = await repository.GetAllHistoryOrderPending();
                var member = _mapper.Map<List<ListOrderCustomerByAdminModel>>(list);
                return Ok(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailByOrderId(int id)
        {
            try
            {
                var list = await repository.GetOrderDetailByOrderId(id);
                var member = _mapper.Map<List<ViewOrderDetailModel>>(list);
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
        public async Task<ActionResult> AddToCart([FromBody] CartItemModel cartItem)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return StatusCode(500, "An error occurred while creating.");
            }
            try
            {
                
                await repository.AddToCart(cartItem.productId, cartItem.customerId);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating.");
            }
        }



        [HttpPost]
        public async Task<ActionResult> ChangeQuantityInCart([FromBody] ChangeQuantityCartItemModel model)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return StatusCode(500, "An error occurred while creating.");
            }
            try
            {

                var result = await repository.ChangeQuantityInCart(model.customerId, model.productId,model.sign);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFromCart([FromBody] CartItemModel model)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return StatusCode(500, "An error occurred while creating.");
            }
            try
            {

                var result = await repository.RemoveFromCart(model.customerId, model.productId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSizeCart(int id)
        {
            try
            {
                var task = await repository.GetSizeCart(id);               
                return Ok(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYearInOrderDate()
        {
            try
            {
                var task = await repository.GetAllYearInOrderDate();
                return Ok(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet("{month}/{year}")]
        public async Task<IActionResult> GetRevenueForMonth(int month,int year)
        {
            try
            {
                var task = await repository.GetRevenueForMonth(month,year);
                return Ok(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

    }
}
