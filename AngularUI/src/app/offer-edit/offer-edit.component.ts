import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Offer } from '../model/Offer';
import { Validators } from '@angular/forms';
import { ConfigService } from '../config/config.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-offer-edit',
  templateUrl: './offer-edit.component.html',
  styleUrls: ['./offer-edit.component.css']
})

export class OfferEditComponent implements OnInit {
  //offer object to save offer details
  offer: Offer = new Offer(0,0,0,"", 0,"", "", new Date(), new Date(), new Date())

  //show errors to user
  pageError: string = ""

  //reactive form
  offerForm: FormGroup = new FormGroup({})

  //jwt token
  token: string | null = ""

  //offer id
  id: number = 0

  //config service - httpClient , route - to navigate
  constructor(private configService: ConfigService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    //get the token
    this.token = localStorage.getItem("token")

    //get the offer id from route param
    this.id = Number(this.route.snapshot.paramMap.get('id'))

    //get the offer details
    if (this.token != null) {
      this.configService.getOfferDetailsById(this.token, this.id).subscribe((data: Offer) => {
        this.offer = data

        //prefill the form with old details
        this.offerForm = new FormGroup({
          details: new FormControl(this.offer.details, [
            Validators.required,
            Validators.minLength(10)
          ]),
          category: new FormControl(this.offer.category, [
            Validators.required
          ])
        })
      }, error => {
        console.log(error)
        this.pageError = "There was some error, Please try again later"
      })
    }
  }

  //get name() { return this.offerForm.get('name') }
  get category() { return this.offerForm.get('category') }
  get details() { return this.offerForm.get('details') }
  
  
  //on submit function
  onSubmit() {
    //save the form details
    this.offer.details = this.offerForm.value.details
    this.offer.category = this.offerForm.value.category
    console.log(this.offer);
    //update the offer details by calling the rest api
    if (this.token != null)
      this.configService.updateOffer(this.token, this.offer).subscribe((data) => {
        this.pageError = "Updated"
        console.warn(data);
      })
  }
}
