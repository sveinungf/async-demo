using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace AsyncDemo.WebApi
{
    public class Program
    {
        public static int Requests;

        public static void Main(string[] args)
        {
            new Thread(ShowThreadStats) { IsBackground = true }.Start();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void ShowThreadStats(object obj)
        {
            ThreadPool.SetMaxThreads(10, 100);

            while (true)
            {
                ThreadPool.GetAvailableThreads(out var workerThreads, out var _);
                ThreadPool.GetMinThreads(out var minThreads, out var _);
                ThreadPool.GetMaxThreads(out var maxThreads, out var _);

                Console.WriteLine($"Available: {workerThreads}, Active: {maxThreads - workerThreads}, Min: {minThreads}, Max: {maxThreads}, Requests: {Requests}");

                Thread.Sleep(1000);
            }
        }
    }
}
