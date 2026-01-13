using DAL.Orders.DbModels;
using DAL.Orders.Extentions;
using DAL.Orders.Services.Base;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;

namespace MyApi.Controllers
{
    public class OrdersController : Controller
    {
        public OrdersController(IOrdersService ordersService, ICustomersService customersService, IProductsService productsService)
        {
            _ordersService = ordersService;
            _customersService = customersService;
            _productsService = productsService;
        }
        private readonly IProductsService _productsService;
        private readonly IOrdersService _ordersService;
        private readonly ICustomersService _customersService;


        [HttpPost]
        public async Task<IActionResult> Buy([FromBody] OrderBuyingModel productBuying)
        {
            var customer = await _customersService.GetByEmail(productBuying.CustomerEmail).GetSingleAsync();
            var product = await _productsService.GetByIds(productBuying.ProductId).GetSingleAsync();


            var newOrder = new Order()
            {
                BoughtAt = DateTime.UtcNow,
                CustomerId = customer.Id,
                Id = -1,
                ProductId = product.Id
            };

            var added = await _ordersService.CreateOrder(newOrder);

            if (!added)
                throw new ApplicationException("Unable to create an order");


            return Ok();
        }
    }
}
