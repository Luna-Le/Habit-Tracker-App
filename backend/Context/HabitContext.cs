using Microsoft.EntityFrameworkCore;
using MyHabitTrackerApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MyHabitTrackerApp.Context;

 public class HabitContext : IdentityDbContext<User>
{
    public HabitContext (DbContextOptions<HabitContext> options)
        : base(options)
    {
    }

    public DbSet<Habit> Habits { get; set; } = default!;


}