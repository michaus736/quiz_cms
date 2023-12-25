import { Component } from '@angular/core';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {
  categories: any[]=[];
  filteredCategories: any[]=[];
  searchTerm: string = '';

  constructor(private categoryService: CategoryHttpService){}

  ngOnInit(): void {
    this.loadCategories();
  }

  private loadCategories():void{
    this.categoryService.showCategories().subscribe(
      (data)=>{
        this.categories=data.model;
        this.filterCategories();
      },
      (error)=>{
        console.error('Error fetching categories',error);
      }
    )
  }

  filterCategories():void{
    if(!this.searchTerm){
      this.filteredCategories=this.categories;
    }else{
      this.filteredCategories=this.categories.filter(category => category.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  }

}
