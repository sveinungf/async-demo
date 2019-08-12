using AsyncDemo.WebApi.Apis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsyncVoidController : ControllerBase
    {
        private readonly FailingApi _api;

        public AsyncVoidController(FailingApi api)
        {
            _api = api;
        }

        [HttpGet("/async-task-without-await")]
        public string HelloAsyncTaskWithoutAwait()
        {
            try
            {
                _api.CallAsyncTask();
                return "Hello from async Task!";
            }
            catch (Exception)
            {
                return "Exception caught!";
            }
        }

        [HttpGet("/async-void")]
        public string HelloAsyncVoid()
        {
            try
            {
                _api.CallAsyncVoid();
                return "Hello from async void!";
            }
            catch (Exception)
            {
                return "Exception caught!";
            }
        }

        [HttpGet("/async-task")]
        public async Task<string> HelloAsyncTask()
        {
            try
            {
                await _api.CallAsyncTask();
                return "Hello from async Task!";
            }
            catch (Exception)
            {
                return "Exception caught!";
            }
        }
    }
}