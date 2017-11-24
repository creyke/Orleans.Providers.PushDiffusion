using System.Threading.Tasks;
using Orleans.Providers.PushDiffusion.Example.Interfaces;

namespace Orleans.Providers.PushDiffusion.Example.Grains
{
    public class MeterGrain : Grain<long>, IMeterGrain
    {
        public async Task ClearValue()
        {
            await ClearStateAsync();
        }

        public Task<long> GetValue()
        {
            return Task.FromResult(State);
        }

        public async Task SetValue(long value)
        {
            State = value;
            await WriteStateAsync();
        }
    }
}
