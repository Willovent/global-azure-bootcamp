using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.WebJob
{
    internal class ServiceCollectionJobActivator : IJobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceCollectionJobActivator(IServiceCollection serviceCollection)
        {
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T CreateInstance<T>() => _serviceProvider.GetService<T>();
    }
}
