using Orleans.Runtime;

namespace Orleans.Providers.PushDiffusion.Storage
{
    public class PushDiffusionDefaultMapper : IPushDiffusionMapper
    {
        public string CreateTopicPath(string grainType, GrainReference grainReference)
        {
            return "foo/bar";
        }
    }
}
