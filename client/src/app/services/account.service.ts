import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5010/api/';
  currentUser$ = new ReplaySubject<User|null>(1);

  constructor(private http: HttpClient) { }
  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login', model)
    .pipe(map((response: User) => {
      const user = response;
      if(user){
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUser$.next(user);
      }
    }));
  }

  register(model: any){
    return this.http
    .post<User>(this.baseUrl + 'account/register', model)
    .pipe(map(user => {
      if(user){
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUser$.next(user);
      }
    }));
  }

  setCurrentUser(user: User){
    this.currentUser$.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser$.next(null);
  }
}
