using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InMemoryCacheTestController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public InMemoryCacheTestController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult GetCacheValue(string key)
        {
            if (_memoryCache.TryGetValue(key, out string? value))
            {
                return Ok(value);
            }
            return NotFound("Key not found in cache.");
        }

        [HttpPost]
        public IActionResult SetCacheValue(string key, string value)
        {
            _memoryCache.Set(key, value, options: new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(10)
            });
            return Ok("Value set in cache.");
        }
    }
}
