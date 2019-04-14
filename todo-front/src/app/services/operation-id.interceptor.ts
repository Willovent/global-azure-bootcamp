import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoggingService } from './logging.service';

@Injectable()
export class OperationIdIntercerptor implements HttpInterceptor {

  constructor(private loggingService: LoggingService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({
      setHeaders: { 'Request-Id': this.loggingService.getOperationId() }
    });

    return next.handle(req);
  }

}
