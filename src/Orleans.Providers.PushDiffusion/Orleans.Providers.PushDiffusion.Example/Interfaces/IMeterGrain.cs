using System.Threading.Tasks;

namespace Orleans.Providers.PushDiffusion.Example.Interfaces
{
    public interface IMeterGrain : IGrainWithIntegerKey
    {
        Task ClearValue();
        Task<long> GetValue();
        Task SetValue(long value);
    }
}
