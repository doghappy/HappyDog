import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-admin-sign-in',
  templateUrl: './admin-sign-in.component.html',
  styleUrls: ['./admin-sign-in.component.css']
})
export class AdminSignInComponent {

  constructor() { }

  @Input()
  public userName: string;

  @Input()
  public password: string;

  @Input()
  public rememberMe: boolean;

  public signIn(): void {
    console.log('UserName: ' + this.userName);
    console.log('Password: ' + this.password);
    console.log('rememberMe: ' + this.rememberMe);
  }
}
