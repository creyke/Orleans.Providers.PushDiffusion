using Microsoft.Extensions.Logging;
using Orleans.Hosting;
using Orleans.Providers.PushDiffusion.Example.Grains;
using Orleans.Providers.PushDiffusion.Example.Interfaces;
using Orleans.Providers.PushDiffusion.Example.Providers;
using Orleans.Runtime.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Providers.PushDiffusion.Example
{
    class Program
    {
        private static Example example;

        static async Task Main(string[] args)
        {
            example = new Example();

            var silo = BuildSilo();
            await silo.StartAsync();

            var client = await CreateGrainClient();

            await example.Run(client);

            Console.ReadLine();
            await silo.StopAsync();
        }

        // https://medium.com/@scott.rangeley/from-the-ground-up-basics-of-porting-to-orleans-2-0-beta-af6824990d77
        private static ISiloHost BuildSilo()
        {
            var localConfiguration = ClusterConfiguration.LocalhostPrimarySilo();

            example.RegisterProvider(localConfiguration);

            return new SiloHostBuilder()
                .UseConfiguration(localConfiguration)
                .ConfigureLogging(logging => logging.AddConsole())
                .AddApplicationPartsFromReferences(typeof(MeterGrain).Assembly)
                .Build();
        }

        

        private static async Task<IClusterClient> CreateGrainClient()
        {
            var configuration = ClientConfiguration.LocalhostSilo();
            var client = new ClientBuilder()
                .UseConfiguration(configuration)
                .ConfigureLogging(logging => logging.AddConsole())
                .AddApplicationPartsFromReferences(typeof(MeterGrain).Assembly)
                .Build();
            await client.Connect();
            return client;
        }
    }
}
