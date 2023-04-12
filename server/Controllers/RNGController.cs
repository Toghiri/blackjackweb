using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/random-number")]
    public class ValuesController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            var random = new Random();
            return Ok(random.Next(1000000));
        }
    }
}
