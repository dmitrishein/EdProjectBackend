import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders} from '@angular/common/http'
import {UserForRegistration} from './../../_interfaces/user/userForRegistration'
import {EnvironmentUrlService} from './environment-url.service'


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private _http: HttpClient, private _envUrl: EnvironmentUrlService) { }

  public registerUser = (route: string, body: UserForRegistration) => {
    return this._http.post(this.createCompleteRoute(route,this._envUrl.urlAddress),body);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
