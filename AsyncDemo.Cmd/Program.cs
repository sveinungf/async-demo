using System;
using System.Threading.Tasks;

namespace AsyncDemo.Cmd
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var uri = new Uri(args[0]);
            var requestGenerator = new RequestGenerator(uri);
            var task = requestGenerator.Run();

            Console.WriteLine("Use up/down arrows to increment/decrement request count");

            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                if (key.Key == ConsoleKey.UpArrow)
                {
                    var requestCount = requestGenerator.IncrementRequestCount();
                    Console.WriteLine($"Request count: {requestCount}");
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    var requestCount = requestGenerator.DecrementRequestCount();
                    Console.WriteLine($"Request count: {requestCount}");
                }

                key = Console.ReadKey();
            }

            requestGenerator.Stop();
            await task;

            Console.WriteLine("Finished");
        }
    }
}
