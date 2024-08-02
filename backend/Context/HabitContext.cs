using Microsoft.EntityFrameworkCore;
using MyHabitTrackerApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MyHabitTrackerApp.Context;

public class HabitDb : IdentityDbContext<User>
{
    public HabitDb(DbContextOptions<HabitDb> options)
        : base(options)
    {
    }

    public DbSet<Habit> Habits { get; set; } = default!;
}