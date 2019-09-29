import { Component } from '@angular/core';
import { UserService } from './services/user.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    constructor(
        protected userService: UserService,
        private router: Router
    ) {
    }

    title = '开心狗 Console';

    signOut(): void {
        this.userService.signOut().subscribe(result => {
            this.userService.isAuth = false;
            this.router.navigate(['signin']);
        });
    }
}
