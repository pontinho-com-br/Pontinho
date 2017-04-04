import { Component, OnInit } from '@angular/core';
import { UserService } from './../../services/user.service';
import { AuthService } from './../../services/auth.service';
import { Router } from '@angular/router';

@Component({
    templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {
    model: any = {};

    constructor(
        private userService: UserService,
        private authService: AuthService,
        private router: Router
    ) { }

    ngOnInit() { }

    login() {
        this.userService.login(this.model).subscribe(result => {
            this.userService.profile().subscribe(profile => {
                this.userService.currentUser = profile;
                this.authService.setCurrentUser(profile);
                if (!!!this.authService.redirectUrl) {
                    this.authService.redirectUrl = '/';
                }
                this.router.navigate([this.authService.redirectUrl]);
            });
        });
    }
}