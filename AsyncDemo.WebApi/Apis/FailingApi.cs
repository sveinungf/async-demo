using System;
using System.Threading.Tasks;

namespace AsyncDemo.WebApi.Apis
{
    public class FailingApi
    {
        public async Task CallAsyncTask()
        {
            await Task.Delay(2000);
            throw new Exception("An async Task error occurred!");
        }

        public async void CallAsyncVoid()
        {
            await Task.Delay(2000);
            throw new Exception("An async void error occurred! Catch me if you can!");
        }
    }
}
