import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'This is a cool dating app';
  users: any;
  constructor(private http: HttpClient){
    this.getUsers();
  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:5010/users')
    .subscribe({
      next: (response) => {this.users = response}, 
      error: (error) => {console.log(error.message)}});
  }
  
}