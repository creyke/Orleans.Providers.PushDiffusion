using System.Threading.Tasks;
using PushTechnology.ClientInterface.Client.Factories;
using Orleans.Runtime;
using Orleans.Storage;
using PushTechnology.ClientInterface.Client.Topics;
using PushTechnology.ClientInterface.Client.Session;
using PushTechnology.ClientInterface.Data.JSON;
using PushTechnology.ClientInterface.Client.Features.Control.Topics;
using Newtonsoft.Json;
using System;

namespace Orleans.Providers.PushDiffusion.Storage
{
    public abstract class PushDiffusionBaseStorageProvider<TMapper> : IStorageProvider
        where TMapper : IPushDiffusionMapper
    {
        private IProviderConfiguration config;
        private TMapper mapper;
        private ISession session;

        public string Name { get; private set; }

        public Logger Log { get; private set; }

        public Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            mapper = Activator.CreateInstance<TMapper>();
            Name = name;
            Log = providerRuntime.GetLogger(name);
            this.config = config;
            return Task.CompletedTask;
        }

        public Task Close()
        {
            if (session != null && session.State.Connected)
            {
                session.Close();
            }

            return Task.CompletedTask;
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var topicPath = await ConnectAndAddTopicIfRequiredAsync(grainType, grainReference);
            await ReadState(grainState, topicPath);
        }

        private Task ReadState(IGrainState grainState, string topicPath)
        {
            // TODO: Implement.
            return Task.CompletedTask;
        }

        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var topicPath = await ConnectAndAddTopicIfRequiredAsync(grainType, grainReference);
            await WriteState(grainState, topicPath);
        }

        private Task WriteState(IGrainState grainState, string topicPath)
        {
            var tcs = new TaskCompletionSource<int>();
            var value = Diffusion.DataTypes.JSON.FromJSONString(JsonConvert.SerializeObject(grainState.State));
            session.TopicUpdateControl.Updater.ValueUpdater<IJSON>().Update(topicPath, value, new PushDiffusionTopicUpdaterUpdateCallback(tcs));
            return tcs.Task;
        }

        public async Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var topicPath = await ConnectAndAddTopicIfRequiredAsync(grainType, grainReference);
            await ClearState(grainState, topicPath);
        }

        private Task ClearState(IGrainState grainState, string topicPath)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }

        private async Task<string> ConnectAndAddTopicIfRequiredAsync(string grainType, GrainReference grainReference)
        {
            ConnectIfRequired();
            return await AddTopicIfRequiredAsync(grainType, grainReference);
        }

        private void ConnectIfRequired()
        {
            if (session != null)
            {
                return;
            }

            // TODO: Handle disconnects.

            session = Diffusion.Sessions
                .Principal(config.GetProperty("Principal", "admin"))
                .Password(config.GetProperty("Password", "password"))
                .Open(config.GetProperty("Url", "ws://localhost:8080"));
        }

        private async Task<string> AddTopicIfRequiredAsync(string grainType, GrainReference grainReference)
        {
            var topicPath = CreateTopicPath(grainType, grainReference);
            await session.TopicControl.AddTopicAsync(topicPath, TopicType.JSON);
            return topicPath;
        }

        private string CreateTopicPath(string grainType, GrainReference grainReference)
        {
            return mapper.CreateTopicPath(grainType, grainReference);
        }
    }
}
