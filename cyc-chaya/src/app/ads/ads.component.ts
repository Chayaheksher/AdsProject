import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, ContentChild, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AdsService } from '../ads.service';
import { ConnectServiceService } from '../connect-service.service';
import { HomePageComponent } from '../home-page/home-page.component';
import { AdDiaryComponent } from '../ad-diary/ad-diary.component';
import { Router } from '@angular/router';

import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ApiService } from '../presetation-goals.service';
import { interval, Subscription } from 'rxjs';
export interface PeriodicElement {
  adNum: number;
  characterIdNeedApproval:number;
  ishurForAdId:number;
  adType: string;
  size: string;
  location: string;
  status: string;
  nowStatusId:number
}

@Component({
  selector: 'app-ads',
  templateUrl: './ads.component.html',
  styleUrls: ['./ads.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class AdsComponent implements OnInit, OnDestroy{
  constructor(private _serviceAd:AdsService, private _serviceConnect:ConnectServiceService, private _route:Router, private _servicePGoals:ApiService,private _liveAnnouncer: LiveAnnouncer){}

  userName:string
  ngOnInit(): void {
    this.userId = this._serviceConnect.user.userId;
    this.charactersId = this._serviceConnect.user.charactersId;
    this.userName = this._serviceConnect.user.userName;
    this.CallAdToUser();
  }
  CallAdToUser(){
     const sub1 =  this._serviceAd.AdToUser(this.userId).subscribe(
      (next:any)=>{
        this.dataSource = new MatTableDataSource<any>(next);
        this.dataSource.sort = this.sort;
        this.filteredOptions = next
        this.dataSource.filterPredicate = (data: PeriodicElement, filter: string) => {
          const filterArray = JSON.parse(filter);
          return filterArray.every(({ column, value }) => {
            return data[column].toString().toLowerCase().includes(value.toLowerCase());
          });
        };
      },
      (error) => {
        console.error('Error fetching data:', error);
      },
    ()=>{}
    )
    this.subs.push(sub1)
  }
  @ViewChild(MatSort) sort: MatSort;

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.active} ${sortState.direction}`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }
  public adRowToDelete: number
  dataSource : any;
  public userId:number;
  public charactersId:number;
  public firstDate:Date;
  public endDate:Date;
  public filteredOptions:any;
  sub:any;
  columnsToDisplay = ['adNum', 'adType', 'size', 'location','status'];
  columnsInHebrew = ['מספר מודעה', 'סוג מודעה', 'גודל', 'מיקום', 'סטטוס']
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  expandedElement: PeriodicElement | null;
  FilterDates():any{
    const firstDateIso = this.firstDate ? this.firstDate.toISOString() : undefined;
    const endDateIso = this.endDate ? this.endDate.toISOString() : undefined;

    this.userId = this._serviceConnect.user.userId
    const sub2 =  this._serviceAd.AdToUser(this.userId, this.firstDate, this.endDate).subscribe(
      (next:any)=>{this.dataSource = next; this.dataSource.sort = this.sort;
        this.filteredOptions = next
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    )
    this.subs.push(sub2)
  }

  applyFilter(event: Event, column: string) {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
  
    // Add the new filter value to the existing filter
    this.dataSource.filterPredicate = (data: PeriodicElement, filter: string) => {
      const filterArray = JSON.parse(filter);
      return filterArray.every(({ column, value }) => {
        return data[column].toString().toLowerCase().includes(value.toLowerCase());
      });
    };
  
    // Get existing filters and add the new one
    const currentFilters = this.dataSource.filter ? JSON.parse(this.dataSource.filter) : [];
    const newFilters = currentFilters.filter(f => f.column !== column); // Remove existing filter for the same column
    newFilters.push({ column, value: filterValue });
    this.dataSource.filter = JSON.stringify(newFilters);
  
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  

  PresentationGoals(){
    const sub3 =  this._servicePGoals.OutApi(this.userId, this.charactersId).subscribe(
      (next:any)=>{
        alert('המודעה התווספה בהצלחה');
        this.CallAdToUser();
      },
     (err:any)=>{console.log('שגיאה')}
    )
    this.subs.push(sub3)
    }
    private subs: Subscription[] = [];
    ngOnDestroy(): void {
      this.subs.forEach((s) => s.unsubscribe());
    }
  }

