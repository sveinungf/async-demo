using AsyncDemo.WebApi.Apis;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParallelController : ControllerBase
    {
        private readonly SlowApi _api;

        public ParallelController(SlowApi api)
        {
            _api = api;
        }

        [HttpGet("/await-directly")]
        public async Task<string> AwaitDirectly()
        {
            var result1 = await _api.CallAsync("one");
            var result2 = await _api.CallAsync("two");
            var result3 = await _api.CallAsync("three");

            return $"Await directly results: {string.Join(", ", result1, result2, result3)}";
        }

        [HttpGet("/await-later")]
        public async Task<string> AwaitLater()
        {
            var task1 = _api.CallAsync("one");
            var task2 = _api.CallAsync("two");
            var task3 = _api.CallAsync("three");

            var result1 = await task1;
            var result2 = await task2;
            var result3 = await task3;

            return $"Await later results: {string.Join(", ", result1, result2, result3)}";
        }

        [HttpGet("/await-all")]
        public async Task<string> AwaitAll()
        {
            var task1 = _api.CallAsync("one");
            var task2 = _api.CallAsync("two");
            var task3 = _api.CallAsync("three");

            var results = await Task.WhenAll(task1, task2, task3);

            return $"Await all results: {string.Join(", ", results)}";
        }
    }
}
