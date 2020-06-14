using System.Threading.Tasks;
using AMR_Server.Application.DeviceVendors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/DeviceVendor")]
    [ApiController]
    public class DeviceVendorController : ApiController
    {
        [Route("GetDeviceVendorDetailsByIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDeviceVendorDetailsByIdAsync(int id)
        {
            var vendor=await Mediator.Send(new GetDeviceVendorDetailsByIdQuery() {VendorId=id });
            return Ok(vendor);
        }
    }
}