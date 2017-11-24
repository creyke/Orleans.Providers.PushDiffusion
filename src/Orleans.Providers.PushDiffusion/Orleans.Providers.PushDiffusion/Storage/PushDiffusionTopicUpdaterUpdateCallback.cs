using PushTechnology.ClientInterface.Client.Callbacks;
using PushTechnology.ClientInterface.Client.Features.Control.Topics;
using System.Threading.Tasks;

namespace Orleans.Providers.PushDiffusion.Storage
{
    public class PushDiffusionTopicUpdaterUpdateCallback : ITopicUpdaterUpdateCallback
    {
        private TaskCompletionSource<int> tcs;

        public PushDiffusionTopicUpdaterUpdateCallback(TaskCompletionSource<int> tcs)
        {
            this.tcs = tcs;
        }

        public void OnError(ErrorReason errorReason)
        {
            tcs.SetException(new PushDiffusionException(errorReason));
            tcs = null;
        }

        public void OnSuccess()
        {
            tcs.SetResult(0);
            tcs = null;
        }
    }
}
