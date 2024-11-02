using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcaFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouthController : ControllerBase
    {

        private readonly IYouthService _youthService;

        public YouthController(IYouthService youthService)
        {
            _youthService = youthService;
        }

        [HttpGet]
        [Route("members")]
        public async Task<IActionResult> Get()
        {
            var youtmembers = await _youthService.GetYouthMembersAsync();

            return Ok(youtmembers);
        }
    }
}
