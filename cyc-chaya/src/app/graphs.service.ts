import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GraphsService {

  constructor(private _http: HttpClient) { }
  ApprovalUsers(selectedMonth):Observable<any>{
     return this._http.get<any>('http://localhost:5263/api/Graphs/ApprovalUsers/'+selectedMonth)
  }
  WhereStatusAds(selectedMonth):Observable<any>{
    return this._http.get<any>('http://localhost:5263/api/Graphs/WhereStatusAds/'+selectedMonth)
 }
 AdCategory(selectedMonth):Observable<any>{
  return this._http.get<any>('http://localhost:5263/api/Graphs/AdCategory/'+selectedMonth)
}
SumCustomerCharge(selectedMonth):Observable<any>{
  return this._http.get<any>('http://localhost:5263/api/Graphs/SumCustomerCharge/'+selectedMonth)
}
}
export interface whereStatusAds{
  adStatusName:string;
  countAdsInStatus: number;
 }
 export interface adCategory{
  adTypesName:string;
  countAdsInType:number;
 }
 export interface sumCustomerCharge{
  customerName:string;
  sumCharge:number;
 }
