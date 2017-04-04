import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ValidationService } from './validation.service';
import 'rxjs/add/operator/catch';

@Injectable()
export class MessageService {
    private BaseEndPoint: string = '/api/message';

    constructor(
        private _http: Http,
        private validationService: ValidationService
    ) { }

    get(): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }
}