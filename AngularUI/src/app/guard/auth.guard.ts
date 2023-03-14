import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ConfigService } from '../config/config.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
 constructor(private authservice : ConfigService,private router:Router){}
 canActivate() {
   if(this.authservice.IsLoggedIn()==true){
     console.log("Activated");
     return true;
   }
   else{
    console.log("Dectivated");

     this.router.navigate(['login']);
     return false;
   }
 }
  
}
