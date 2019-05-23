import { Router } from '@angular/router';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  registerMode = false;

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit() {
    if (this.auth.isLoggedIn) {
      this.router.navigate(['/members']);
    }
  }


}
