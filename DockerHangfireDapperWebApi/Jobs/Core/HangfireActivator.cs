using Hangfire;
using System;

namespace Docker.Jobs.Core
{
    public class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public HangfireActivator(IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        public override object ActivateJob(Type type) =>
            _serviceProvider.GetService(type);
    }

}
