using MyHabitTrackerApp.Models;


namespace  MyHabitTrackerApp.Repositories
{
    public interface IHabitRepository
    {
        Task<IEnumerable<Habit>> GetAllHabitsAsync();
        Task<Habit> GetHabitByIdAsync(long id);
        Task AddHabitAsync(Habit habit);
        Task UpdateHabitAsync(Habit Habit);
        Task DeleteHabitAsync(long id);
        Task<bool> HabitExistsAsync(long id);
        Task BulkAddHabitsAsync(IEnumerable<Habit> habits);
    }
}

