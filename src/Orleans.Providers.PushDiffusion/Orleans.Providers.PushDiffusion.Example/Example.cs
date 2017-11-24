using Orleans.Providers.PushDiffusion.Example.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orleans.Providers.PushDiffusion.Example
{
    public class Example
    {
        public async Task Run(IClusterClient client)
        {
            var random = new Random();

            while (true)
            {
                for (int meterId = 0; meterId < 10; meterId++)
                {
                    var value = random.Next(0, 100);
                    await client.GetGrain<IMeterGrain>(meterId).SetValue(value);
                }

                await Task.Delay(100);
            }
        }
    }
}
