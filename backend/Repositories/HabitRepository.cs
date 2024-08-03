using MyHabitTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using MyHabitTrackerApp.Context;


namespace  MyHabitTrackerApp.Repositories
{
    public class HabitRepository : IHabitRepository
    {
        private readonly HabitContext _context;

        public HabitRepository(HabitContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Habit>> GetAllHabitsAsync(){
            
            return await _context.Habits.ToListAsync();
        
        }
        public async Task<IEnumerable<Habit>> GetAllHabitsByUserIdAsync(string userId){
            
            return await _context.Habits
                .Where(h => h.UserId == userId)
                .Include(h => h.User)
                .ToListAsync();
        
        }
        public async Task<Habit> GetHabitByIdAsync(long id){
            
            return await _context.Habits.FindAsync(id);
        
        }

     
        public async Task AddHabitAsync(Habit habit){
             
            _context.Habits.Add(habit);
            await _context.SaveChangesAsync();
        
        }  
           
        public async Task UpdateHabitAsync(Habit Habit)
         {
            _context.Entry(Habit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHabitAsync(long id)
         {
            var habit= await _context.Habits.FindAsync(id);
            if (habit != null)
            {
                _context.Habits.Remove(habit);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> HabitExistsAsync(long id)
        {
            return await _context.Habits.AnyAsync(e => e.Id == id);
        }
        public async Task BulkAddHabitsAsync(IEnumerable<Habit> habits)
         {
            await _context.Habits.AddRangeAsync(habits);
            await _context.SaveChangesAsync();
        }
    }
}

