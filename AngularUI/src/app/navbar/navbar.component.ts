import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfigService } from '../config/config.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
offers:any;
token:any;
person = { 
  "empid":1, 
  "password":"12345" 
}; 
  //to store the localStorage 
  local:any
  constructor(private route:Router,private configdata:ConfigService) {
    this.token=configdata.getUserToken(this.person);

    configdata.getOffers(this.token,"Electronics").subscribe(data=>{
    console.log(data);
   })

   }
  ngOnInit(): void {
    this.local = localStorage
  }

  //logout function
  logout(){

    //call the auth service and reroute to login page
    localStorage.clear()
    this.route.navigate(['/login']);
  }

  


}
