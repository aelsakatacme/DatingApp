import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { UserDTO } from '../_models/UserDTO';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  userDTO: UserDTO;
  @Output() emitCancel: EventEmitter<void> = new EventEmitter<void>();

  constructor(private auth: AuthService, private alertify: AlertifyService, private router: Router) {
    this.userDTO = new UserDTO();
  }

  ngOnInit() { }

  submit() {
    this.auth.register(this.userDTO).subscribe(x => {
      this.alertify.success('registered successfully');
      this.emitCancel.emit();
    }, err => {
      this.alertify.error('error when register');
    });
  }

  cancel() {
    this.emitCancel.emit();
  }

}
