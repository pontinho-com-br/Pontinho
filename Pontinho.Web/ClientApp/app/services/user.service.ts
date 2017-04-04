import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ValidationService } from './validation.service';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class UserService {
    private BaseEndPoint: string = '/api/user';

    constructor(
        private _http: Http,
        private validationService: ValidationService
    ) { }

    currentUser: any;

    login(user: any): Observable<any> {
        return this._http.post(`${this.BaseEndPoint}/login`, user)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    register(user: any): Observable<any> {
        console.log(user);
        return this._http.post(`${this.BaseEndPoint}/register`, user)
            .catch(e => this.validationService.handleError(e));
    }

    profile(): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}/profile`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    updateProfile(profile: any): Observable<any> {
        return this._http.post(`${this.BaseEndPoint}/profile`, profile)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }
}