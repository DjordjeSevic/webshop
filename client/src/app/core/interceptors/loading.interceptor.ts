import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';
import { resourceUsage } from 'process';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (!request.url.includes('emailExists') || request.method === 'POST' && request.url.includes('orders')) {
      return next.handle(request);
    }

    this.busyService.busy();

    return next.handle(request).pipe(
      delay(300),
      finalize(() => {this.busyService.idle()})
    );
  }
}
