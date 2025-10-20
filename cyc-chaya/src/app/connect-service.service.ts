import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConnectServiceService {
  private connectUrl = 'http://localhost:5263/Connection';
  constructor(private _http: HttpClient) {

  }
  public user: user

  UserLogin(userName: string, userPassword: number): Observable<user> {
    return this._http.get<user>(`${this.connectUrl}/UserLogin/${userName}/${userPassword}`);
  }
}


export interface user {
  userId: number,
  userName: string,
  charactersId: number,
  charactersName: string
}