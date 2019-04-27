using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Todo.Core.Initializers
{
    public class AppNameInitializer : ITelemetryInitializer
    {
        private readonly string _appName;

        public AppNameInitializer(string appName)
        {
            _appName = appName;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                //set custom role name here
                telemetry.Context.Cloud.RoleName = _appName;
            }
        }
    }
}
