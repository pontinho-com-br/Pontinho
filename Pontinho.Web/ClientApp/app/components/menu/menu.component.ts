import { Component, OnInit } from '@angular/core';
import { AuthService } from './../../services/auth.service';

@Component({
    selector: 'p-menu',
    template: `
    <div class="pageContent" *ngIf="userId">
        <div class="container">
            <ul class="topNavigation">
                <li>
                    <div class="btn-group simpleList list-sm">
                        <button type="button" class="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class="zmdi zmdi-settings zmdi-hc-fw icon"></i>
                        <!--<span class="badge">98</span>-->
                    </button>
                        <ul class="dropdown-menu pull-right">
                            <li>
                                <a class="clearfix" routerLink="/user/profile">
                                    <span class="pull-left"><i class="fa fa-user" aria-hidden="true"></i> My Profile</span>
                                    <!--<span class="pull-right info">109,073</span>-->
                                </a>
                            </li>
                            <li>
                                <a class="clearfix" routerLink="/player/list">
                                    <span class="pull-left"><i class="fa fa-users" aria-hidden="true"></i> Players</span>
                                    <!--<span class="pull-right info">109,073</span>-->
                                </a>
                            </li>
                            <li>
                                <a href="/Home/Logout" title="#" class="clearfix">
                                    <span class="pull-left"><i class="fa fa-sign-out" aria-hidden="true"></i> Logout</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
        </div>
    </div>
    `
})

export class MenuComponent implements OnInit {
    userId: string;
    constructor(private authService: AuthService) {
        this.authService.userChange.subscribe(userId => {
            this.userId = userId;
        });
    }

    ngOnInit() { }
}