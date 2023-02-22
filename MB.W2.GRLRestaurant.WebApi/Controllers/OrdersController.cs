using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.W2.GRLRestaurant.EFCore;
using MB.W2.GRLRestaurant.Entities;
using AutoMapper;
using MB.W2.GRLRestaurant.Dtos.Orders;

namespace MB.W2.GRLRestaurant.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Services, Actions, Endpoints

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderListDto>>> GetOrders()
        {
            var orders = await _context
                                    .Orders
                                    .Include(order => order.Customer)
                                    .ToListAsync();

            var orderDtos = _mapper.Map<List<OrderListDto>>(orders);

            return orderDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrder(int id)
        {
            var order = await _context
                                .Orders
                                .Include(order => order.Customer)
                                .Include(order => order.Meals)
                                    .ThenInclude(meal => meal.Ingredients)
                                .SingleOrDefaultAsync(order => order.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDetailsDto>(order);

            return orderDto;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            await AddMealsToOrder(orderDto.MealIds, order);

            order.TotalPrice = GetOrderTotalPrice(order.Meals);

            order.OrderDate = DateTime.Now;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(orderDto);

            order.OrderDate = await GetOrderDateAsync(order.Id);

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                await UpdateOrderMeals(orderDto.MealIds, order.Id);

                order.TotalPrice = GetOrderTotalPrice(order.Meals);

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Private Methods

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task AddMealsToOrder(List<int> mealIds, Order order)
        {
            var meals = await _context.Meals.Where(meal => mealIds.Contains(meal.Id)).ToListAsync();
            
            order.Meals.AddRange(meals); 
        }

        private double GetOrderTotalPrice(List<Meal> meals)
        {
            return meals.Sum(meal => meal.Price);
        }

        private async Task UpdateOrderMeals(List<int> mealIds, int orderId)
        {
            var order = await _context
                             .Orders
                             .Include(o => o.Meals)
                             .SingleAsync(o => o.Id == orderId);

            order.Meals.Clear();

            var meals = await _context
                            .Meals
                            .Where(m => mealIds.Contains(m.Id))
                            .ToListAsync();

            order.Meals.AddRange(meals);
        }

        private async Task<DateTime> GetOrderDateAsync(int id)
        {
            return await _context
                                .Orders
                                .Where(o => o.Id == id)
                                .Select(o => o.OrderDate)
                                .SingleAsync();
        }

        #endregion
    }
}
