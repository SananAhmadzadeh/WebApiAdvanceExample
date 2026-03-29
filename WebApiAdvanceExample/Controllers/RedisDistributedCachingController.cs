using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisDistributedCachingController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        public RedisDistributedCachingController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var name =await  _distributedCache.GetStringAsync("name");
            var surnamByte = await _distributedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(surnamByte);
            if (name == null && surname == null)
            {
                return NotFound("Key not found in cache.");
            }
            return Ok(new
            {
                name,
                surname
            });
        }

        [HttpPost]
        public IActionResult Set(string name, string surname)
        {
            _distributedCache.SetString("name", name, options: new()
            {
                AbsoluteExpiration = DateTime.UtcNow.AddSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(300)
            });

            _distributedCache.Set("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration = DateTime.UtcNow.AddSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(300)
            });

            return Ok("Value set in cache.");
        }
    }
}
