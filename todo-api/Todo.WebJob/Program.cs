using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Todo.Core.Clients;

namespace Todo.WebJob
{
    class Program
    {
        static void Main()
        {
            var config = new JobHostConfiguration("UseDevelopmentStorage=true");

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
