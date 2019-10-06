using AsyncDemo.WebApi.Apis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PitfallController : ControllerBase
    {
        private readonly SlowApi _api;

        public PitfallController(SlowApi api)
        {
            _api = api;
        }

        [HttpGet("/pitfall-1")]
        public async Task<string> Pitfall1()
        {
            try
            {
                var result = await _api.CallAsync();
                return $"Got this on first try: {result}";
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                var result = await _api.CallAsync();
                return $"It failed, got it on second try: {result}";
            }
        }

        [HttpGet("/pitfall-2")]
        public async Task<string> Pitfall2()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ForEach(async x => await _api.CallAsync(x));
            return "It's done, I don't care about the result";
        }

        [HttpGet("/pitfall-3")]
        public Task<string> Pitfall3()
        {
            try
            {
                return _api.CallAsync();
            }
            catch (Exception)
            {
                return Task.FromResult("It failed!");
            }
        }
    }
}
