using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMR_Server.Application.UserBasicDatas.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/UserBasicData")]
    [ApiController]
    public class UserBasicDataController : ApiController
    {
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserBasicDataCommand command)
        {
            var UserBasicDatas = await Mediator.Send(command);
            return Ok(UserBasicDatas);
        }
    }
}