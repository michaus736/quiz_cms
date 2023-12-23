import { Injectable } from "@angular/core";
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})

export class AuthService{
    
    constructor(private router: Router) { }

    isUserLoggedIn(): boolean{
        return !!localStorage.getItem('jwtToken');
    }


    login(token: string): void {
        localStorage.setItem('jwtToken', token);
    }

    logout(): void {
        localStorage.removeItem('jwtToken')
        this.router.navigate(['/home'])
    }

    getJWTToken(): string{
        return localStorage.getItem('jwtToken')??'';
    }

}