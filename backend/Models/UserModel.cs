using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;


namespace MyHabitTrackerApp.Models;

public class User: IdentityUser{
  
     [JsonIgnore]
     public ICollection<Habit>? Habits { get; set; }

    
}