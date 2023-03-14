//import { ThisReceiver, ThrowStmt } from '@angular/compiler';
//import { ifStmt } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfigService } from '../config/config.service';
import { messageResponse } from '../model/messageResponse';
import { Offer } from "../model/Offer";

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.css']
})

export class OfferDetailsComponent implements OnInit {

  //offer object to save the offer details 
  offer: Offer = new Offer(2,0,0,"status", 0,"Category", "details", new Date(), new Date(), new Date())
  
  //to toggle the like button
  isLiked: boolean = false

  //to handle error
  pageError: String = ""

  //config service : httpClient , route : to retrive the id param 
  constructor(private route: ActivatedRoute, private configService: ConfigService) { }

  ngOnInit(): void {
    //retrieve token and ids
    let token = localStorage.getItem("token")
    let id = Number(this.route.snapshot.paramMap.get("id"))
    let empId = Number(localStorage.getItem("userId"))

    //call the service which calls the rest apis
      this.configService.getOfferDetailsById(" ", id).subscribe((data: Offer) => {
        
        //save the details in the offer object
        this.offer = data
      })
  }

  //function to toggle the like button 
  likedIt() {
    this.isLiked = !this.isLiked
  }

  //save user like
  submitLike() {
    let token = localStorage.getItem("token")
    let id = Number(this.route.snapshot.paramMap.get("id"))
    let empId = Number(localStorage.getItem("userId"))

    if (token != null)
      this.configService.saveLike(token, id).subscribe((data) => {
        console.log("like",data);
        this.pageError = "your like is saved successfully"
        this.isLiked = false;
        this.offer.likes += 1
      },error=>{
        console.log(error)
        this.pageError = "Some error occured please try again later"
      })
  }

  
}


