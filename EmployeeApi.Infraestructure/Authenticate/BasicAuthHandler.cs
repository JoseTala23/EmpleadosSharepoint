using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeApi.Domain.Model;
using EmpleadosSharepoint;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace EmployeeApi.Infraestructure.Authenticate
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private IUser _userService;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUser user
            ): base( options ,logger,encoder,clock)
        {
            _userService = user;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No se encuentra la cabecera de autentificacion");

            bool result = false;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var creatialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(creatialBytes).Split(new[] { ':' }, 2);
                var userName = credentials[0];
                var password = credentials[1];
                result = _userService.IsUser(userName, password);
            }
            catch 
            {
                return AuthenticateResult.Fail("No se encuentra los parámetros de autentificacion");
            }

            if (!result)
            {
                return AuthenticateResult.Fail("autentificacion no válida");
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,"id"),
                new Claim(ClaimTypes.Name,"user"),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
