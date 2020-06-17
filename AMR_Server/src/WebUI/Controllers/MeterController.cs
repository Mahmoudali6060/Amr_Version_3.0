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
        public async Task<IActionResult> GetAllMetersAsync(int pageSize, int currentPage, string keyword)
        {
            var meters = await Mediator.Send(new GetAllMetersQuery() { PageSize = pageSize, CurrentPage = currentPage, Keyword = keyword });
            return Ok(meters);
        }
    }
}