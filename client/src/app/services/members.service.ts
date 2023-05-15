import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baserUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<Member[]>(this.baserUrl + 'users');
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baserUrl + "users/" + username);
  }
}
