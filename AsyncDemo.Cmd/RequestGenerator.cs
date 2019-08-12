using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.Cmd
{
    internal class RequestGenerator
    {
        private readonly Uri _uri;
        private volatile bool _stop = false;
        private volatile int _requestCount;

        public RequestGenerator(Uri uri) => _uri = uri;

        public int IncrementRequestCount() => Interlocked.Increment(ref _requestCount);
        public int DecrementRequestCount() => _requestCount > 0 ? Interlocked.Decrement(ref _requestCount) : _requestCount;
        public void Stop() => _stop = true;

        public async Task Run()
        {
            var tasks = new List<(Task Task, CancellationTokenSource TokenSource)>();

            while (!_stop)
            {
                if (_requestCount > tasks.Count)
                {
                    var tokenSource = new CancellationTokenSource();
                    var task = ContinuouslyKeepInFlightRequest(tokenSource.Token);
                    tasks.Add((task, tokenSource));
                }
                else if (_requestCount < tasks.Count)
                {
                    var tokenSource = tasks[tasks.Count - 1].TokenSource;
                    tokenSource.Cancel();
                    tokenSource.Dispose();
                    tasks.RemoveAt(tasks.Count - 1);
                }

                await Task.Delay(100);
            }

            foreach (var tokenSource in tasks.Select(x => x.TokenSource))
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
        }

        private async Task ContinuouslyKeepInFlightRequest(CancellationToken token)
        {
            using (var httpClient = new HttpClient { BaseAddress = _uri, Timeout = TimeSpan.FromSeconds(3) })
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        using (var response = await httpClient.GetAsync(""))
                        {
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
