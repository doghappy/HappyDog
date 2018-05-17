import { Component } from '@angular/core';
import { Category } from './models/category';
import { Configuration } from './data/configuration';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor() {
    this.categories = Configuration.categories;
  }

  public categories: Category[];
}
