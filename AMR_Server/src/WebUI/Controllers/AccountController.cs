using AMR_Server.Application.Account.Commands.Login;
using AMR_Server.Application.Account.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/Account")]
    [ApiController]
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginCommand>> Login(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
