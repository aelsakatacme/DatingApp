import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { UserDTO } from '../_models/UserDTO';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from './alertify.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl + 'auth/';
  private jwtHelper = new JwtHelperService();

  public currentUser: UserDTO;
  public isLoggedIn: boolean;
  public jwtToken: string;


  constructor(private http: HttpClient, private alertify: AlertifyService, private router: Router) {
    this.currentUser = this.getUser();
    this.isLoggedIn = this.userIsLogged();
    this.jwtToken = this.getToken();
  }


  login(userDTO: UserDTO) {
    return this.http.post(this.baseUrl + 'login', userDTO).pipe(
      map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.token.toString());
          localStorage.setItem('user', JSON.stringify(response.user));
          this.currentUser = this.getUser();
          this.isLoggedIn = this.userIsLogged();
          this.jwtToken = this.getToken();
        }
      })
    );
  }

  private getUser(): UserDTO {
    // get username from token :)
    // const decodedToken = this.jwtHelper.decodeToken(localStorage.getItem('token'));
    // this.currentUser = new UserDTO();
    // this.currentUser.username = decodedToken.unique_name;
    return localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')) as UserDTO : null;
  }

  private userIsLogged(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  private getToken(): string {
    const token = localStorage.getItem('token');
    if (!this.jwtHelper.isTokenExpired(token)) {
      return 'Bearer ' + token;
    }
    return '';
  }

  logout() {
    localStorage.setItem('token', '');
    localStorage.setItem('user', '');
    this.isLoggedIn = this.userIsLogged();
    this.currentUser = this.getUser();
    this.alertify.success('logged out successfully');
    this.router.navigate(['/home']);
  }

  register(user: UserDTO) {
    return this.http.post(this.baseUrl + 'register', user);
  }

}
