using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMR_Server.Application.Meters.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/Meter")]
    [ApiController]
    public class MeterController : ApiController
    {
        [Route("GetAllMetersAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAllMetersAsync()
        {
            var meters=await Mediator.Send(new GetAllMetersQuery());
            return Ok(meters);
        }
    }
}