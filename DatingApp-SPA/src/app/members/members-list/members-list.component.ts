import { UserDTO } from '../../_models/UserDTO';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.component.html',
  styleUrls: ['./members-list.component.scss']
})
export class MembersListComponent implements OnInit {
  users: UserDTO[];

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe(res => { this.users = res; }, err => {
      this.alertify.error('You don\'t have permission');
    });
  }

}
