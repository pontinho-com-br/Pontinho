import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ValidationService } from './validation.service';
import 'rxjs/add/operator/catch';

@Injectable()
export class PlayerService {
    private BaseEndPoint: string = '/api/player';

    constructor(
        private _http: Http,
        private validationService: ValidationService
    ) { }

    getAll(): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    update(player: any): Observable<any> {
        return this._http.post(`${this.BaseEndPoint}`, player)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    delete(id: number): Observable<any> {
        return this._http.delete(`${this.BaseEndPoint}?id=${id}`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    get(id: number): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}?id=${id}`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    getForCompetition(id: number): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}/competition/${id}`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    getMePlayers(): Observable<any> {
        return this._http.get(`${this.BaseEndPoint}/me`)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }

    setAsMe(id: number): Observable<any> {
        return this._http.post(`${this.BaseEndPoint}/setAsMe/${id}`, null)
            .map((response: Response) => response.json())
            .catch(e => this.validationService.handleError(e));
    }
}