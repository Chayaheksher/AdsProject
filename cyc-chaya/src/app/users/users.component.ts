import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { UsersDetailsService } from '../users-details.service';
import { UsersDetailsInterface } from '../users-details-interface';
import { EditOrNewComponent } from '../edit-or-new/edit-or-new.component';
import { MatDialog, MAT_DIALOG_DATA  } from '@angular/material/dialog';
import { ScrollStrategyOptions, BlockScrollStrategy, Overlay } from '@angular/cdk/overlay';
import { interval, Subscription } from 'rxjs';


export interface PeriodicElement {
 fullName: string;
  userName: string;
  charactersName: string;
  lastEnterDate: Date;
  contant:string;
  edit:void;
}

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent implements OnInit, OnDestroy {
displayedColumns: string[] = ['fullName', 'userName', 'charactersName', 'lastEnterDate', 'contant','edit'];
public dataSource:PeriodicElement[];
public ind:number=-1;
 constructor(private _usersDetails:UsersDetailsService, public dialog: MatDialog,private overlay: Overlay, private scrollStrategyOptions: ScrollStrategyOptions){}
  ngOnInit(): void {
    this.ToGetUser();
  }
  ToGetUser(){
    const sub1 =  this._usersDetails.GetUsers().subscribe(
      (next:any)=>{this.dataSource=next.users;
      },
      (err:any)=>{err; alert('אירעה שגיאה')}
    )
    this.subs.push(sub1);
  }
openDialog(enterAnimationDuration: string, exitAnimationDuration: string, rowIndex: number): void {
  const scrollStrategy = this.overlay.scrollStrategies.block();

const dialogRef=this.dialog.open(EditOrNewComponent, {
    data: { rowIndex }, 
    scrollStrategy:scrollStrategy
  });
  const sub2 = dialogRef.componentInstance.refreshRequested.subscribe(() => {
    this.ToGetUser()
  });
this.subs.push(sub2);
}
private subs: Subscription[] = [];
ngOnDestroy(): void {
  this.subs.forEach((s) => s.unsubscribe());
}
}