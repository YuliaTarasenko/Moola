﻿namespace Moola.IntegrationsTests
{ 
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, "Test admin"),
                    new(ClaimsIdentity.DefaultRoleClaimType, "Admin")
                };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "TestScheme");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }
    }
}