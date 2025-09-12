using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;
    private ClaimsPrincipal _current = new(new ClaimsIdentity());

    public JwtAuthStateProvider(IJSRuntime js) { _js = js; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "authToken");
        _current = BuildPrincipal(token);
        return new AuthenticationState(_current);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await _js.InvokeVoidAsync("sessionStorage.setItem", "authToken", token);
        _current = BuildPrincipal(token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_current)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _js.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
        _current = new(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_current)));
    }

    private static ClaimsPrincipal BuildPrincipal(string? token)
    {
        if (string.IsNullOrWhiteSpace(token)) return new(new ClaimsIdentity());
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var identity = new ClaimsIdentity(jwt.Claims, authenticationType: "jwt");
        return new ClaimsPrincipal(identity);
    }
}
