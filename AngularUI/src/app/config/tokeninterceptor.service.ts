import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class TokeninterceptorService implements HttpInterceptor{

  constructor(private inject:Injector) { }
  intercept(req: HttpRequest<any>, next:HttpHandler): Observable<HttpEvent<any>>{
    let authservice = this.inject.get(ConfigService);
    let jwtToken = req.clone({
      setHeaders:{
        Authorization: 'Bearer '+ authservice.getToken()
      }
    });
    return next.handle(jwtToken);
    

  }

}
