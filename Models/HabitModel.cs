namespace MyHabitTrackerApp.Models;

public enum FrequencyType
{
    Daily,
    Weekly,
    Monthly
}
public class Habit
{
    public long Id { get; set; }

    public FrequencyType FrequencyType { get; set; }
    public long FrequencyCount {get; set;}
    public string? HabitName {get; set;}

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? LastCompleted { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; } 

    public User User { get; set; } = null!;
}
