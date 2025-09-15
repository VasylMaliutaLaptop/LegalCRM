using LegalCRM.Data;
using Microsoft.AspNetCore.Identity;

namespace LegalCRM.Api.Services;
public class UserService(UserManager<User> userManager)
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<IdentityResult> RegisterUserAsync(string userName, string email, string password)
    {
        var user = new User
        {
            UserName = userName,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);
        return result;
    }
    public async Task<bool> ValidateCredentialsAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return false;
        return await _userManager.CheckPasswordAsync(user, password);
    }
}
