import { Component } from '@angular/core';
import { ConfigService } from './config/config.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {



  person = { 
    "empid":1, 
    "password":"12345" 
  }; 
    //to store the localStorage 
    local:any
  token: any;
    constructor(private configdata:ConfigService) {
     this.token=configdata.getUserToken(this.person);
     configdata.getOffers(this.token,"Electronics").subscribe(data=>{
      console.log(data);
     })
     }
  
}
