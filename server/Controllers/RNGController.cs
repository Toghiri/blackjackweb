using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Blackjack.Controllers
{
    [Route("api/random-number")]
    public class RNGController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            byte[] rand = RandomNumberGenerator.GetBytes(4);
            uint i = BitConverter.ToUInt32(rand, 0);
            uint result = i % 1000000;
            return Ok(result);
        }
    }
}
