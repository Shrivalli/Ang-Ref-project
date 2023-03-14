import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { ConfigService } from '../config/config.service';
import { AuthResponse } from '../model/authResponse';
import { User } from '../model/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  constructor(private configService: ConfigService,private router:Router) { }
  user: User = { empid:0, password: "" }
  authResponse:AuthResponse=new AuthResponse("","")
  userForm: FormGroup = new FormGroup({})
  formError = ""

  ngOnInit(): void {
    this.userForm = new FormGroup({
      empid: new FormControl(this.user.empid, [
        Validators.required,
        Validators.minLength(1)
      ]),
      password: new FormControl(this.user.password, [
        Validators.required,
      ])
    })
  }

  get empid() { return this.userForm.get('empid') }
  get password() { return this.userForm.get('password') }

  responsedata :any;
  onSubmit() {
    
    let userDetails = { "EmployeeId": this.userForm.value.empid, "Password": this.userForm.value.password}
    
    this.configService.getUserToken(userDetails).subscribe((data:AuthResponse)=>{
      this.responsedata=data;
      localStorage.setItem("token",this.responsedata.token)
      
      localStorage.setItem("userId",userDetails.EmployeeId)

      this.router.navigate(['main'])
    },
    error =>{
      this.formError = "Credentials are incorrect"
      console.log(error)
    });

  }
}
