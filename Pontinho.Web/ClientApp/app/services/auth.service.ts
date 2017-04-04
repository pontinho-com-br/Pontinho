import { Injectable, EventEmitter } from '@angular/core';
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Subject } from "rxjs/Subject";

@Injectable()
export class AuthService {
    isLoggedIn: boolean = false;
    redirectUrl: string;

    private userKey = "pontinho_user";
    private user: any;
    userChange = new BehaviorSubject<string>(null);

    constructor() {
        let user = localStorage.getItem(this.userKey);
        this.setCurrentUser(JSON.parse(user));
    }

    logout(): void {
        this.setCurrentUser(null);
    }

    setCurrentUser(user: any) {
        this.isLoggedIn = !!user;
        this.user = user;
        if (this.user == null) {
            localStorage.removeItem(this.userKey);
        } else {
            localStorage.setItem(this.userKey, JSON.stringify(user));
        }
        this.userChange.next(this.user);
    }
}
