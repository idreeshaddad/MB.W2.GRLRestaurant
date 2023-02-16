using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.W2.GRLRestaurant.EFCore;
using MB.W2.GRLRestaurant.Entities;
using AutoMapper;
using MB.W2.GRLRestaurant.Dtos.Customers;

namespace MB.W2.GRLRestaurant.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Services

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDetailsDto>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            var customerDtos = _mapper.Map<List<CustomerDetailsDto>>(customers);

            return customerDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailsDto>> GetCustomer(int id)
        {
            var customer = await _context
                                    .Customers
                                    .Include(c => c.Orders)
                                    .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDetailsDto>(customer);

            return customerDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer(int id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<Customer>(customerDto);

            // _context.Update(customer);
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customerDto.Id }, customerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Private Methods

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        } 

        #endregion
    }
}
