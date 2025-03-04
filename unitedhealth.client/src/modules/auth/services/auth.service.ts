import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { JwtResponse } from '../models';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private userSubject: BehaviorSubject<JwtResponse | null>;
  public user: Observable<JwtResponse | null>;

  constructor(private http: HttpClient, private router: Router) { 
    this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  public get userValue () { 
    return this.userSubject.value;
  }

  // cookie-based login
  public signIn(username: string, password: string) {
    return this.http.post(`${environment.apiServerUrl}/auth/login`, {
      username: username,
      password: password
    }).pipe(map(user => {
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.setItem('user', JSON.stringify(user));
      this.userSubject.next(user);
      return user;
    }));
  }

  // register new user
  public register(formObject: any) {
    return this.http.post(`${environment.apiServerUrl}/auth/register`, formObject);
  }

  // sign out
  public signOut() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/auth/login']);   
  }

}
