import { Component, OnInit } from '@angular/core';
import { SignInDto } from '../models/user/signin-dto';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-signin',
    templateUrl: './signin.component.html',
    styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

    constructor(
        private userService: UserService,
        private router: Router
    ) { }

    dto: SignInDto;
    message: string;
    isActive: boolean;

    ngOnInit() {
        this.dto = new SignInDto();
    }

    signIn(): void {
        this.isActive = true;
        this.userService.signIn(this.dto).subscribe(r => {
            this.isActive = false;
            this.router.navigate(['/']);
        }, err => {
            this.message = err.error.message;
            this.isActive = false;
        });
    }
}
