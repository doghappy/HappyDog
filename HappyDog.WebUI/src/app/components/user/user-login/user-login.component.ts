import { Component, Input } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { CodeResult } from '../../../models/results/codeResult';
import { HttpBaseResult } from '../../../models/results/httpBaseResult';

@Component({
  selector: 'app-sign-in',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserSignInComponent {

  constructor(private userService: UserService) { }

  @Input()
  public userName: string;

  @Input()
  public password: string;

  @Input()
  public rememberMe: boolean = false;

  public loginResult: HttpBaseResult;

  public login(): void {
    this.userService.login(this.userName, this.password, this.rememberMe)
      .subscribe(r => this.loginResult = r);
  }
}
