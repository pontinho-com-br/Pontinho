import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ValidationService } from './validation.service';
import 'rxjs/add/operator/catch';

@Injectable()
export class RoundService {
    private BaseEndPoint: string = '/api/round';

    constructor(
        private _http: Http,
        private validationService: ValidationService
    ) { }    

    delete(id:number): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}/deleteLastFromMatch/${id}`)
            .catch(e => this.validationService.handleError(e));
    }    

    save(model: any): Observable<any> {
        return this._http.post(`${this.BaseEndPoint}`, model)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }
}