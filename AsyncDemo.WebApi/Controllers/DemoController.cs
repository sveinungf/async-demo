using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet("sync")]
        public string Hello()
        {
            Thread.Sleep(2000);
            return "Hello World";
        }

        [HttpGet("sync-over-async")]
        public string HelloSyncOverAsync()
        {
            Task.Delay(2000).Wait();
            return "Hello World";
        }

        [HttpGet("async-over-sync")]
        public async Task<string> HelloAsyncOverSync()
        {
            await Task.Run(() => Thread.Sleep(2000));
            return "Hello World";
        }

        [HttpGet("async")]
        public async Task<string> HelloAsync()
        {
            await Task.Delay(2000);
            return "Hello World";
        }
    }
}
