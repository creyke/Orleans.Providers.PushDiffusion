using PushTechnology.ClientInterface.Client.Callbacks;
using System;

namespace Orleans.Providers.PushDiffusion.Storage
{
    public class PushDiffusionException : Exception
    {
        public ErrorReason ErrorReason { get; private set; }

        public PushDiffusionException(ErrorReason errorReason)
        {
            ErrorReason = errorReason;
        }
    }
}
