import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AdsService } from '../ads.service';
import { AdsComponent } from '../ads/ads.component';
import { ActivatedRoute, Params } from '@angular/router';
import { interval, Subscription } from 'rxjs';

export interface PeriodicElement {
  dateAndTime: Date;
  userName: string;
  publicationDates:Date;
  ishurType: string;
  approvalOrRejection: string;
  note:string;
}

@Component({
  selector: 'app-ad-diary',
  templateUrl: './ad-diary.component.html',
  styleUrls: ['./ad-diary.component.css']
})
export class AdDiaryComponent implements OnInit, OnDestroy{
  displayedColumns: string[] = ['dateAndTime', 'userName', 'publicationDates', 'ishurType', 'approvalOrRejection', 'note'];
  dataSource:any;
  private subs: Subscription[] = [];
  @Input() adId:any;
  constructor(private _serviceAds:AdsService,private actRoute: ActivatedRoute){}
  ngOnInit(): void {
    const sub1 =  this._serviceAds.AdDiary(this.adId).subscribe(
      (next:any)=>{this.dataSource = next},
      (error) => {
        console.error('Error fetching data:', error);}
    )
    this.subs.push(sub1)
  }
  ngOnDestroy(): void {
    this.subs.forEach((s) => s.unsubscribe());
  }
  
}
