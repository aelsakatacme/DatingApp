import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { UserDTO } from './../_models/UserDTO';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.apiUrl + 'users/';

  constructor(private http: HttpClient) { }


  getUsers(): Observable<UserDTO[]> {
    return this.http.get<UserDTO[]>(this.baseUrl + 'getusers');
  }

  getUser(id: number): Observable<UserDTO> {
    return this.http.get<UserDTO>(this.baseUrl + 'getuser/' + id);
  }

  updateUser(user: UserDTO): Observable<UserDTO> {
    return this.http.put<UserDTO>(this.baseUrl + 'UpdateUser', user);
  }
}
