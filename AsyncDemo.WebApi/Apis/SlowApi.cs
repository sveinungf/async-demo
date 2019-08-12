using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Apis
{
    public class SlowApi
    {
        public string CallSync()
        {
            Thread.Sleep(2000);
            return "Hello world";
        }

        public async Task<string> CallAsync()
        {
            await Task.Delay(2000);
            return "Hello world";
        }

        public async Task<string> CallAsync(string value)
        {
            Console.WriteLine("Called API with value " + value);
            await Task.Delay(2000);
            return value;
        }
    }
}
