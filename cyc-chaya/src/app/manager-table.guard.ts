import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterState, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ConnectServiceService } from './connect-service.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ManagerTableGouardGuard implements CanActivate {
  constructor(private _servise: ConnectServiceService, private _route: Router) { }
  canActivate(
    rout: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this._servise.user.charactersName === "מנהל") {
      return true;
    }
    else if (this._servise.user.charactersName === "מזכירה" || this._servise.user.charactersName === "מעמד" || this._servise.user.charactersName === "גרפיקאית") {
      this._route.navigate(['manager/ads']);
      return true;
    }
    return false;
  }
}
