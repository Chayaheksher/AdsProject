import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterState, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ConnectServiceService } from './connect-service.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
providedIn:'root'
})

export class userGouardGuard implements CanActivate{
  constructor(private _servise:ConnectServiceService, private _route:Router){}
  canActivate (
    rout:ActivatedRouteSnapshot,
    state:RouterStateSnapshot):Observable<boolean | UrlTree>|Promise<boolean|UrlTree>|boolean|UrlTree{
      if(this._servise.user.charactersName==="מנהל"||this._servise.user.charactersName==="מזכירה"||this._servise.user.charactersName==="מעמד"||this._servise.user.charactersName==="גרפיקאית"){
        return true;
      }
      else {
        this._route.navigate(['home']);
        return false;
      }
    }
}