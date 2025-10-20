import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ConnectServiceService } from '../connect-service.service';
import { AdsService, StatusForRejectionInterface } from '../ads.service';
import { Route, Router } from '@angular/router';
import { throttleTime } from 'rxjs';
import { interval, Subscription } from 'rxjs';


@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css']
})
export class StatusComponent implements OnInit, OnDestroy {
  constructor(private _serviceConnect: ConnectServiceService, private _serviceAd: AdsService, private _route: Router) { }
  ngOnInit(): void {
    this.userId = this._serviceConnect.user.userId
    this.charactersId = this._serviceConnect.user.charactersId
    const sub1 = this._serviceAd.StatusForRejection(this.adId).subscribe(
      (next: any) => {
        this.forBitul = next.map(element => element);

      },
      (error: any) => { console.log(error) }
    )
    this.subs.push(sub1);
  }

  SaveIshurOrDchiya() {
    if (this.note === null) this.note = '';
    const sub2 = this._serviceAd.AdApproval(this.adId, this.userId, this.theNowStatus, this.ishurOrDchiya, this.note).subscribe(
      (next: any) => {
        if (next === true)
          alert("המודעה עודכנה בהצלחה")
        else if (next === false) alert("חסר חומרים או תאריך פרסום לא עבר");
        this.childCall.emit(true);
      },
      (err: any) => { console.log("error") }
    )
    this.subs.push(sub2);
  }
  @Output() childCall: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Input() theNowStatusName: number;
  @Input() theNowStatus: number;
  @Input() adId: number;
  @Input() characterIdNeedApproval: number;
  public forBitul: StatusForRejectionInterface[]
  public userId: number;
  public charactersId: number;
  public ishurOrDchiya: boolean = true;
  public note: string = 'אין';
  @Input() adRowToDelete: number;
  public adRowToUpdate: number;
  BitulApprovalOrRejection(ishurForAdRowToUpdate: number) {
    const sub3 = this._serviceAd.CancelApprovalOrRejection(this.adRowToDelete, ishurForAdRowToUpdate, this.adId, this.userId).subscribe(
      (next: any) => {
        alert("עבר בהצלחה!!"),
        this.childCall.emit(true);
      },
      (err: any) => { alert(err) }
    )
    this.subs.push(sub3);
  }
  private subs: Subscription[] = [];
  ngOnDestroy(): void {
    this.subs.forEach((s) => s.unsubscribe());
  }
}
