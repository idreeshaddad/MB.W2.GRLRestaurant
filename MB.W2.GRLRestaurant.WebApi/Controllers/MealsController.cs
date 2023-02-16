using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.W2.GRLRestaurant.EFCore;
using MB.W2.GRLRestaurant.Entities;
using AutoMapper;
using MB.W2.GRLRestaurant.Dtos.Meals;

namespace MB.W2.GRLRestaurant.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MealsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Services, Actions, Endpoints

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealListDto>>> GetMeals()
        {
            var meals = await _context.Meals.ToListAsync();

            var mealDtos = _mapper.Map<List<MealListDto>>(meals);

            return mealDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealDetailsDto>> GetMeal(int id)
        {
            var meal = await _context
                                .Meals
                                .Include(m => m.Ingredients)
                                .SingleOrDefaultAsync(m => m.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            var mealDto = _mapper.Map<MealDetailsDto>(meal);

            return mealDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMeal(int id, Meal meal)
        {
            if (id != meal.Id)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Also log to a logging file
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MealDto>> CreateMeal(MealDto mealDto)
        {
            var meal = _mapper.Map<Meal>(mealDto);


            if(mealDto.IngredientIds.Count > 0)
            {
                await AddIngredientsToMeal(mealDto.IngredientIds, meal);
            }


            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            if (_context.Meals == null)
            {
                return NotFound();
            }
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Private Methods

        private bool MealExists(int id)
        {
            return (_context.Meals?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task AddIngredientsToMeal(List<int> ingredientIds, Meal meal)
        {
            var ingredients = await _context
                                    .Ingredients
                                    .Where(i => ingredientIds.Contains(i.Id))
                                    .ToListAsync();

            meal.Ingredients.AddRange(ingredients);
        }

        #endregion
    }
}
