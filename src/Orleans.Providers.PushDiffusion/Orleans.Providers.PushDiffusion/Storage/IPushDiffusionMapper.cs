using Orleans.Runtime;

namespace Orleans.Providers.PushDiffusion.Storage
{
    public interface IPushDiffusionMapper
    {
        string CreateTopicPath(string grainType, GrainReference grainReference);
    }
}
