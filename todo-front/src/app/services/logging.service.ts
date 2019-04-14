import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { ApplicationInsights, SeverityLevel, PropertiesPlugin } from '@microsoft/applicationinsights-web';

@Injectable({ providedIn: 'root' })
export class LoggingService {
  appInsights: ApplicationInsights;

  constructor() {
    this.appInsights = new ApplicationInsights({
      config: {
        instrumentationKey: environment.applicationInsightKey
      }
    });
    this.appInsights.loadAppInsights();
  }

  logTrace(message: string, properties?: { [key: string]: any }, severityLevel?: SeverityLevel) {
    this.appInsights.trackTrace({ message, properties, severityLevel });
  }

  logPageView(name: string, isLoggedIn: boolean, uri: string) {
    this.appInsights.trackPageView({ name, isLoggedIn, uri });
  }

  getOperationId() {
    return this.appInsights.context.telemetryTrace.traceID;
  }
}
