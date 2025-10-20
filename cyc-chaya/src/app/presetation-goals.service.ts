import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = 'https://boi.org.il/PublicApi';

  constructor(private http: HttpClient) { }

  getInterestData(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetInterest`);
  }

  getExchangeRate(key: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetExchangeRate?key=${key}`);
  }

  OutApi(userId:number, charactersId:number):Observable<any>{
    const apiUrl = `http://localhost:5263/api/Ads/OutApi/${userId}/${charactersId}`;
    return this.http.get(apiUrl,{responseType: 'text'})
  }
}
