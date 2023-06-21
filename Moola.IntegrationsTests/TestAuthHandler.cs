namespace Moola.IntegrationsTests
{ 
    public class AdminAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public AdminAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            => AuthTestExtensions.HandleAuthenticationForRole("Admin");
    }

    public class UserAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public UserAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            => AuthTestExtensions.HandleAuthenticationForRole("User");
    }

    public static class AuthTestExtensions
    {
        public static Task<AuthenticateResult> HandleAuthenticationForRole(string role)
        {
            var claims = new[]
                        {
                    new Claim(ClaimTypes.Name, $"Test {role}"),
                    new(ClaimsIdentity.DefaultRoleClaimType, role)
                };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "TestScheme");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }
    }
}
