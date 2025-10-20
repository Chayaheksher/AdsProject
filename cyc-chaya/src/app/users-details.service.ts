import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { UsersDetailsInterface, UsersDetailsInterface1 } from './users-details-interface';

@Injectable({
  providedIn: 'root'
})
export class UsersDetailsService {

  constructor(private _http:HttpClient) {}
  private _url: string='http://localhost:5263/api/UsersDetails'
public GetUsers(): Observable<void> {
return this._http.get<void>(this._url)
}
GetRoleName():Observable<void>{
  return this._http.get<void>(this._url+ '/api/rolenames')
}
GetComName():Observable<void>{
  return this._http.get<void>(this._url+ '/communication/comName')
}
GetCom():Observable<void>{
  return this._http.get<void>(this._url+ '/communication')
}
SaveUserDetails(theData:any):Observable<void>{
  return this._http.post<any>('http://localhost:5263/api/UsersDetails/saveUserDetails', theData).pipe(
    catchError((error) => {
      console.error('Error in SaveUserDetails:', error);
      throw error;
    })
  );;
}
}
