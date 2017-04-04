import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ValidationService } from './validation.service';
import 'rxjs/add/operator/catch';

@Injectable()
export class CompetitionService {
    private BaseEndPoint: string = '/api/competition';

    constructor(
        private _http: Http,
        private validationService: ValidationService
    ) { }

    get(id: number): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}/?id=${id}`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    getAll(): Observable<any[]> {
        return this._http.get(`${this.BaseEndPoint}`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    getDashboard(): Observable<any> {
        return this._http.get(`/dashboard`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    save(model: any): Observable<any> {
        return this._http.post(`${this.BaseEndPoint}`, model)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }
}