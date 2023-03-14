import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from "@angular/forms";
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfigService } from '../config/config.service';
import { Offer } from '../model/Offer';

@Component({
  selector: 'app-add-offer',
  templateUrl: './add-offer.component.html',
  styleUrls: ['./add-offer.component.css']
})

export class AddOfferComponent implements OnInit {
  //offer form
  addOfferForm: FormGroup = new FormGroup({})
  
  //error handling
  pageError: String = ""
  emp:any
  //offer object to save the offer details
  offer: Offer = new Offer(0,0,0,"", 0,"", "", new Date(), new Date(), new Date())
  
  //jwt token
  token: string | null = ""
  
  constructor(private configService: ConfigService,private route:Router) { }

  ngOnInit(): void {

    //retrieve the token
    this.token = localStorage.getItem("token")
    
    //initiate the form
    this.addOfferForm = new FormGroup({
      details: new FormControl("", [
        Validators.required,
        Validators.minLength(10)
      ]),
      offerId:new FormControl(),
      employeeId:new FormControl(),
      status:new FormControl(),

      category: new FormControl("Plants", [
        Validators.required
      ])
    })
  }

  get details() { return this.addOfferForm.get('details') }

  get category() { return this.addOfferForm.get('category') }

  onSubmit() {
    this.emp=localStorage.getItem("userId")
    //update the offer values
    this.offer.offerId = this.addOfferForm.value.offerId
    this.offer.employeeId = this.emp
    this.offer.status = this.addOfferForm.value.status
    this.offer.details = this.addOfferForm.value.details
    this.offer.category = this.addOfferForm.value.category
    console.log(this.offer)
    if (this.token != null)
      this.configService.addOffer(this.token, this.offer).subscribe((data) => {
        this.route.navigate(["/myOffers"])
      })
  }
}
