import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent {
  category!: Category;
  categoryId: string = '';
  updateSuccess = false;
  errors: string[] = [];


  constructor(private categoryHttpService: CategoryHttpService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.categoryId = params['id'];
      this.getCategoryData();
    });
    this.getCategoryData();
  }

  getCategoryData(): void {
    this.categoryHttpService.showCategory(this.categoryId).subscribe(
      (Data: any) => {
        console.log(Data);
        this.category = Data.model;
      },
      (error) => {
        console.error('Error fetching user:', error);
      }
    );
  }

  updateCategory(): void {
    this.categoryHttpService.update(this.category).subscribe(
      response => {
        console.log('Category updated successfully', response);
        this.updateSuccess = true;
        this.errors = [];
        setTimeout(() => this.updateSuccess = false, 5000);
      },
      error => {
        this.updateSuccess = false;
        if (error.error && error.error.errors) {
          this.errors = Object.keys(error.error.errors).flatMap(k => error.error.errors[k]);
        } else {
          this.errors = ['Wystąpił błąd podczas aktualizacji danych kategorii.'];
        }
      }
    );
  }

  deleteCategory(categoryId: string | undefined): void {
    if (!categoryId) {
      console.error("categoryId undefined");
      return;
    }

    this.categoryHttpService.delete(categoryId).subscribe(
      response => {
        console.log('Category deleted successfully', response);
        // Przekierowanie po pomyślnym usunięciu
        this.router.navigate(['/moderator/category']);
      },
      error => {
        if (error.error && error.error.errors) {
          this.errors = Object.keys(error.error.errors).flatMap(k => error.error.errors[k]);
        } else {
          this.errors = ['Wystąpił błąd podczas usuwania kategorii.'];
        }
      }
    );
  }
  
  
}

