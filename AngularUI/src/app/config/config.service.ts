import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { AuthResponse } from "../model/authResponse";
import { Offer } from "../model/Offer";
import { Employee } from "../model/Employee";
import { messageResponse } from '../model/messageResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  //authentication microservice
  // loginserviceUrl = 'https://authorizationmicroservicess.azurewebsites.net/api/Authenticate'

  // //offer microservice
  // offerserviceUrl = "https://offermicroservicessclassifieds.azurewebsites.net/api/Offer"

  // //employee microservice
  // employeeserviceUrl ="https://employeemicroserviceclassified.azurewebsites.net/api/Employee"

  // //points microservice
  // pointsserviceUrl = "https://pointsmicroservices.azurewebsites.net/api/Points"
  //authentication microservice

  loginserviceUrl = "https://localhost:44353/api/Auth"
  //offer microservice
  offerserviceUrl = "https://localhost:44362/api/Offer"
  //employee microservice
  employeeserviceUrl ="https://localhost:44349/api/Employee"
  //points microservice
  pointsserviceUrl = "https://localhost:44365/api/Points"

  constructor(private http: HttpClient,private router:Router) { }

  //------------------------authentication microservice calls -----------------------
  getUserToken(empDetails: any) {
    
    return this.http.post<AuthResponse>(this.loginserviceUrl, empDetails);
  }
  saveTokens(tokendata:any){
    localStorage.setItem('token',tokendata.token)
  }
  IsLoggedIn(){
    console.log(localStorage.getItem('token'));
      return localStorage.getItem('token')!=null;
  }
  Logout(){
    alert("Sorry session expired");
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }
  getToken(){
    //const token = localStorage.getItem('token');
    return localStorage.getItem('token')||'';
  }

  //--------------------offer microservice calls --------------------------------
  getOffers(token: string, category: string) {
    let options = {
      headers: { "Authorization": "Bearer " + token}
    }
    return this.http.get<Offer[]>(this.offerserviceUrl + "/GetOfferByCategory/" + category,options);
  
  }
  getOffersByTopLikes(token: string) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.get<Offer[]>(this.offerserviceUrl + "/GetOfferByTopThreeLikes",options);
  }
  getOffersByPostedDate(token: string, postedDate: string) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.get<Offer[]>(this.offerserviceUrl + "/GetOfferByOpenedDate/" + postedDate,options);
  }
  getOfferDetailsById(token: string, id: number) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.get<Offer>(this.offerserviceUrl + "/GetOfferById/" + id,options);
  }
  updateOffer(token: String, offer: Offer) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.put<Offer>(this.offerserviceUrl + "/EditOffer", offer,options);
  }
  addOffer(token: String, offer: Offer) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.post<Offer>(this.offerserviceUrl + "/PostOffer",offer,options);
  }
  engageOffer(token: String, offerId: number, empId: number) {
    let options = {
      headers: { "Authorization": "Bearer " + token },
      params: { "offerId": offerId, "employeeId": empId }
    }
    return this.http.post<Offer>(this.offerserviceUrl + "/engageOffer", null, options);
  }
  //---------------------------------employee microservice calls -------------------------------
  saveLike(token: string, id: number) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.post<Offer>(this.offerserviceUrl + "/LikeOffer/" +id+"?token="+token,  options);
  }
  getProfile(token: String, id: number) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.get<Employee>(this.employeeserviceUrl + "/GetEmployeeProfile/" +id+"?token="+token, options);
  }
  getMyOffers(token: String, id: number) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    console.log(options);
    return this.http.get<Offer[]>(this.employeeserviceUrl + "/ViewEmployeeOffers/" + id+"?token="+token,options);
  }
  getRecentlyLiked(token: string) {
    let options = {
      headers: { "Authorization": "Bearer " + token },
    }
    return this.http.get<Offer[]>(this.employeeserviceUrl + "/recentlyLiked",options);
  }
  //---------------------------------points microservice calls ----------------------------------
  updatePoints(token: String, id: number) {
    let options = {
      headers: { "Authorization": "Bearer " + token }
    }
    return this.http.get<Employee>(this.pointsserviceUrl + "/RefreshPointsByEmployee/" + id+"?token="+token, options);

  }

}