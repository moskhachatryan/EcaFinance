using BLL.Services.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcaFinance.Controllers.Api
{
    [Route("api/youth")]
    [ApiController]
    public class YouthMembersController : ControllerBase
    {

        private readonly IYouthService _youthService;

        public YouthMembersController(IYouthService youthService)
        {
            _youthService = youthService;
        }

        // GET: api/<YouthMembersController>
        [HttpGet]
        [Route("members")]
        public async Task<IActionResult> Get()
        {
            var youtmembers = await _youthService.GetYouthMembersAsync();

            return Ok(youtmembers);
        }

        // GET api/<YouthMembersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<YouthMembersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<YouthMembersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<YouthMembersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
