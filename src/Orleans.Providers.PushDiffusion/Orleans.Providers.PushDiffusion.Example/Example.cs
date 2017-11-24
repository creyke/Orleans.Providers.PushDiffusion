using Orleans.Providers.PushDiffusion.Example.Interfaces;
using Orleans.Providers.PushDiffusion.Example.Providers;
using Orleans.Runtime.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.Providers.PushDiffusion.Example
{
    public class Example
    {
        public void RegisterProvider(ClusterConfiguration localConfiguration)
        {
            localConfiguration.Globals.RegisterStorageProvider<ExamplePushDiffusionStorageProvider>(
            "Default",
            new Dictionary<string, string>
            {
                { "Url","ws://localhost:8080" },
                { "Principal","admin" },
                { "Password","password" }
            });
        }

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
