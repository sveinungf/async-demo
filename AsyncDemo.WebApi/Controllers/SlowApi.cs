using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Controllers
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
    }
}
