using KitchenRouting.Domain;
using KitchenRouting.DTO;
using KitchenRouting.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRouting.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRoutingService _service;
        public OrdersController(IOrderRoutingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Receives an order from a POS system and routes each order item
        /// to its corresponding kitchen area queue.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequestDTO request,
            CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
            {
                return BadRequest("Order must contain at least one item.");

            }

            if (request.Items.Any(i => string.IsNullOrWhiteSpace(i.Name)))
            {
                return BadRequest("All order items must have a valid name.");
            }

            var order = new Order(
                Guid.NewGuid(),
                request.Items.Select(i => new OrderItem(i.Name, i.Area)).ToList(),
                DateTime.UtcNow
                );

            await _service.RouteAsync(order, cancellationToken);

            return Accepted();
        }

    }
}
