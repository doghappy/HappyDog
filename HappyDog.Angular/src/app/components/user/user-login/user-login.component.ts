import { Component, Input } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { CodeResult } from '../../../models/results/codeResult';
import { HttpBaseResult } from '../../../models/results/httpBaseResult';
import { Location } from '@angular/common'

@Component({
  selector: 'app-login-in',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent {

  constructor(
    private userService: UserService,
    private location: Location) {
  }

  @Input()
  public userName: string;

  @Input()
  public password: string;

  @Input()
  public rememberMe: boolean = false;

  public loginResult: HttpBaseResult;

  public login(): void {
    this.loginResult = null;
    this.userService.login(this.userName, this.password, this.rememberMe)
      .subscribe(r => {
        this.loginResult = r;
        if (r.code == CodeResult.OK) {
          this.location.back();
        }
      });
  }
}
