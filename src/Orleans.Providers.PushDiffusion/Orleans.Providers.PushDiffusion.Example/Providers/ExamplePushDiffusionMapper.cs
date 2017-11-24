using Orleans.Providers.PushDiffusion.Storage;
using Orleans.Runtime;

namespace Orleans.Providers.PushDiffusion.Example.Providers
{
    public class ExamplePushDiffusionMapper : IPushDiffusionMapper
    {
        public string CreateTopicPath(string grainType, GrainReference grainReference)
        {
            return $"my/custom/path/{grainType}/{grainReference.GetPrimaryKey()}";
        }
    }
}
