import { Component, OnInit } from '@angular/core';
import { UserService } from './../../services/user.service';

@Component({
    templateUrl: 'register.component.html'
})

export class RegisterComponent implements OnInit {
    model: any = {};

    constructor(private userService: UserService) { }
    register() {
        this.userService.register(this.model).subscribe(result => {
            this.userService.currentUser = result;
        });
    }
    ngOnInit() { }
}