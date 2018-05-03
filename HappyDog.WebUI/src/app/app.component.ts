import { Component } from '@angular/core';
import { CategoryService } from './services/category.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { Category } from './models/category';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private categoryService: CategoryService) { }

  categories: Category[]

  ngOnInit(): void {
    //this.getCategories();
  }

  //getCategories(): void {
  //  this.categoryService.getCategories()
  //    .subscribe(c => this.categories = c)
  //}
}
