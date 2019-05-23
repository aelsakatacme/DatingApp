import { Component, OnInit } from '@angular/core';
import { UserDTO } from '../_models/UserDTO';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styles: []
})
export class NavComponent implements OnInit {

  userLoginDTO: UserDTO = new UserDTO();

  constructor(private auth: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() { }

  login() {
    this.auth.login(this.userLoginDTO).subscribe(next => {
      this.alertify.success('logged in successfully!');
      this.router.navigate(['/members']);
    }, err => {
      this.alertify.error('fail to login!');
    });
  }

}
