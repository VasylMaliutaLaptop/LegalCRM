using LegalCRM.Data;
using Microsoft.AspNetCore.Identity;

namespace LegalCRM.Api.Services;
public class UserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

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
}
