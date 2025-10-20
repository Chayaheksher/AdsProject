import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdsService {
  constructor(private _http: HttpClient) { }

  AdToUser(userId: number, firstDate?: Date, endDate?: Date): Observable<any> {

    let url = `http://localhost:5263/api/Ads/AdToUser/${userId}`;

    const params: any = {};
    if (firstDate && endDate) {
      params['firstDate'] = firstDate.toISOString();
      params['endDate'] = endDate.toISOString();
    }

    return this._http.get<any>(url, { params: params });
  }

  AdDiary(adId: number): Observable<void> {
    return this._http.get<void>('http://localhost:5263/api/Ads/AdDiary/' + adId)
  }
  AdApproval(adId: number, userId: number, currectAdStatus: number, isApproval: boolean, ishurForAdNote: string): Observable<void> {
    return this._http.get<void>('http://localhost:5263/api/Ads/AdApproval/' + adId + '/' + userId + '/' + currectAdStatus + '/' + isApproval + '/' + ishurForAdNote)
  }
  StatusForRejection(adId: number) {
    return this._http.get<StatusForRejectionInterface>(`http://localhost:5263/api/Ads/StatusForRejection?adId=${adId}`);
  }
  private apiUrl = 'http://localhost:5263/api/Ads';
  CancelApprovalOrRejection(adRowToDelete: number, adRowToUpdate: number, adId: number, userId: number): Observable<any> {
    const url = `${this.apiUrl}/CancelApprovalOrRejection/${adRowToDelete}/${adRowToUpdate}/${adId}/${userId}`;
    return this._http.get(url, { responseType: 'text' });
  }
}
export interface StatusForRejectionInterface {
  executionDate: Date,
  ishurForAdRow: number,
  userName: string,
  approvalOrRejection: string,
  approvalType: string
}

