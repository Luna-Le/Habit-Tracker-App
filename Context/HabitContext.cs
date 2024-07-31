using Microsoft.EntityFrameworkCore;
using MyHabitTrackerApp.Models;

namespace MyHabitTrackerApp.Context;

public class HabitDb : DbContext
{
    public HabitDb(DbContextOptions<HabitDb> options)
        : base(options)
    {
    }

    public DbSet<Habit> Habits { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
}