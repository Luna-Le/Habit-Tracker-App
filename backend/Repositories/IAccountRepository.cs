using MyHabitTrackerApp.Models;
using Microsoft.AspNetCore.Identity;



namespace  MyHabitTrackerApp.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(User user, string password);
        Task<SignInResult> LoginAsync(String userName, string password, bool rememberMe);

        
    
      
    }
}
