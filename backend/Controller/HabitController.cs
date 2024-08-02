using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHabitTrackerApp.Context;
using MyHabitTrackerApp.Models;
using MyHabitTrackerApp.Repositories;
using Microsoft.AspNetCore.Identity;



namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly IHabitRepository _repository;
        private readonly HabitDb _context;
         private readonly UserManager<User> _userManager;

       
        public HabitController(IHabitRepository repository, HabitDb context,UserManager<User> userManager  )
        {
            _repository = repository;
            _context = context;
            _userManager = userManager;
        }
         

        // GET: api/Habits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habit>>> GetHabits()
        {
           var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var habits = await _repository.GetAllHabitsByUserIdAsync(userId);
            return Ok(habits);

        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habit>> GetHabit(long id)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            var habit = await _repository.GetHabitByIdAsync(id);

            if (habit == null)
            {
                return NotFound();
            }

            return Ok(habit);
        }

        // PUT: api/Habits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabit(long id, Habit habit)
        {
            if (id != habit.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateHabitAsync(habit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.HabitExistsAsync(id))
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

        // POST: api/Habits
        [HttpPost]
        public async Task<ActionResult<Habit>> PostHabit(Habit habit)
        {
      
            await _repository.AddHabitAsync(habit);
            return CreatedAtAction("GetHabit", new { id = habit.Id }, habit);
        }

        // DELETE: api/Habits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabit(long id)
        {
            var student = await _repository.GetHabitByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await _repository.DeleteHabitAsync(id);

            return NoContent();
        }

        // POST: api/Habits/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<Habit>>> BulkCreateHabits(IEnumerable<Habit> habits)
        {
   
            if (habits == null || !habits.Any())
            {
                return BadRequest("Habit data is required.");
            }
 

            await _repository.BulkAddHabitsAsync(habits);

            return Ok(habits);
        }
    }
}