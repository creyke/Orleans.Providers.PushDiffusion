using Orleans.Providers.PushDiffusion.Storage;
using Orleans.Runtime;
using System.Linq;

namespace Orleans.Providers.PushDiffusion.Example.Providers
{
    public class ExamplePushDiffusionMapper : IPushDiffusionMapper
    {
        public string CreateTopicPath(string grainType, GrainReference grainReference)
        {
            return $"my/custom/path/{grainType.Split('.').Last().Replace("Grain", "").ToLowerInvariant()}/{grainReference.GetPrimaryKeyLong()}";
        }
    }
}
