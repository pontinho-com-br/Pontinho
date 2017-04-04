import { Component, OnInit } from '@angular/core';
import { AuthService } from './../../services/auth.service';
import { UserService } from './../../services/user.service';

@Component({
    selector: 'p-menu',
    template: `
    <div class="pageContent" *ngIf="user">
        <div class="container">
            <ul class="topNavigation">
                <li>
                    <div class="btn-group simpleList list-sm profile profile-image">
                        <button type="button" class="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <img [src]="userService.getGravatarByEmail(user.email)" alt=":)" class="img-responsive">
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
    user: string;
    constructor(
        private authService: AuthService,
        private userService: UserService
        ) {
        this.authService.userChange.subscribe(user => {
            this.user = user;
        });
    }

    ngOnInit() { }
}