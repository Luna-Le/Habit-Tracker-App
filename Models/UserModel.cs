using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace MyHabitTrackerApp.Models;

public class User{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;
    public string? Password { get; set; }

     [JsonIgnore]
     public ICollection<Habit>? Habits { get; set; }

    
}