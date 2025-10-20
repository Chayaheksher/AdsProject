import { group } from '@angular/animations';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConnectServiceService, user } from '../connect-service.service';
import { HttpClient } from '@angular/common/http';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'], 
})
export class HomePageComponent implements OnInit, OnDestroy {
  hide = true;
  public login!:FormGroup;
  public errRole:string="";
  public user1:user;
  ngOnInit(): void {
    this.login = new FormGroup({
      userName:new FormControl('', [Validators.required]),
      userPassword:new FormControl('', [Validators.required, Validators.pattern(/[0-9]/)])
    })
  }
  src:string
  constructor(public router:Router, private _conService:ConnectServiceService){this.src="./image.jpg"}
loginFunc() {
  const sub1= this._conService.UserLogin(this.login.controls['userName'].value, this.login.controls['userPassword'].value)
    .subscribe({
      next: (user:any) => {
        this._conService.user=user.ook.result;
        this.user1 = user.ook.result
        if (this.user1.charactersName === 'מנהל'||this.user1.charactersName==="מזכירה"||this.user1.charactersName==="מעמד"||this.user1.charactersName==="גרפיקאית") {
          this.router.navigate(['/manager']);
          alert(this.user1.charactersName);
        } else if (this.user1.charactersName === 'שם לא קיים') {
          alert("לא קיים במערכת");
        }
      },
      error: (err) => {
        console.error(err);
        alert("אירעה שגיאה בשליפת התפקיד");
      }
    });
    this.subs.push(sub1);
}
private subs: Subscription[] = [];
ngOnDestroy(): void {
  this.subs.forEach((s) => s.unsubscribe());
}
}
