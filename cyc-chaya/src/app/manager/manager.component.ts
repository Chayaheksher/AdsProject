import { Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConnectServiceService } from '../connect-service.service';


@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.css'],
})
export class ManagerComponent implements OnInit{
public isManager:string;
charactersId:number
userName:string
constructor(private _routeManag:ActivatedRoute, private _servise1:ConnectServiceService ){
  this.isManager= _servise1.user.charactersName;
  this.userName = _servise1.user.userName;
  this.charactersId = _servise1.user.charactersId;
};
  ngOnInit(): void {
  }

}
