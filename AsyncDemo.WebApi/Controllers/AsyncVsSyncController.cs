using AsyncDemo.WebApi.Apis;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsyncVsSyncController : ControllerBase
    {
        private readonly SlowApi _api;

        public AsyncVsSyncController(SlowApi api)
        {
            _api = api;
        }

        [HttpGet("/sync")]
        public string HelloSync()
        {
            var result = _api.CallSync();
            return result;
        }

        [HttpGet("/sync-over-async")]
        public string HelloSyncOverAsync()
        {
            var result = _api.CallAsync().Result;
            return result;
        }

        [HttpGet("/async-over-sync")]
        public async Task<string> HelloAsyncOverSync()
        {
            var result = await Task.Run(() => _api.CallSync());
            return result;
        }

        [HttpGet("/async")]
        public async Task<string> HelloAsync()
        {
            var result = await _api.CallAsync();
            return result;
        }
    }
}
