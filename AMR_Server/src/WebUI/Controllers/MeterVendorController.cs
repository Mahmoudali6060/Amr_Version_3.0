using System.Threading.Tasks;
using AMR_Server.Application.MeterVendors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/MeterVendor")]
    [ApiController]
    public class MeterVendorController : ApiController
    {
        [Route("GetVedndorDetailsByIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetVedndorDetailsByIdAsync(int id)
        {
            var vendor=await Mediator.Send(new GetVedndorDetailsByIdQuery() {VendorId=id });
            return Ok(vendor);
        }
    }
}