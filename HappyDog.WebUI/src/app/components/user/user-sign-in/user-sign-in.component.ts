import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-sign-in',
  templateUrl: './user-sign-in.component.html',
  styleUrls: ['./user-sign-in.component.css']
})
export class UserSignInComponent {

  constructor() { }

  @Input()
  public userName: string;

  @Input()
  public password: string;

  @Input()
  public rememberMe: boolean = false;

  public signIn(): void {
    console.log('UserName: ' + this.userName);
    console.log('Password: ' + this.password);
    console.log('rememberMe: ' + this.rememberMe);
  }
}
