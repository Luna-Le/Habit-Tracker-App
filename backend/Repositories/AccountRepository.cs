
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyHabitTrackerApp.Models;


namespace  MyHabitTrackerApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
    
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


       public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync (string userName, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);

        }


    }
    
}