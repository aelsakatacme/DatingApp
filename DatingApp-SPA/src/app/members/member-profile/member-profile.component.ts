import { AlertifyService } from './../../_services/alertify.service';
import { Lookup } from './../../_models/Lookup';
import { LookupService } from './../../_services/lookup.service';
import { AuthService } from './../../_services/auth.service';
import { UserDTO } from './../../_models/UserDTO';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryImage, NgxGalleryOptions, NgxGalleryAnimation } from 'ngx-gallery';


// tslint:disable: align
@Component({
  selector: 'app-member-profile',
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.scss']
})
export class MemberProfileComponent implements OnInit {
  notExist = false;
  user: UserDTO;

  showEditBtn = false;
  editMode = false;

  genderLookup: Lookup[];
  cityLookup: Lookup[];
  countryLookup: Lookup[];

  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private route: ActivatedRoute, private auth: AuthService, private lookupService: LookupService,
    private userService: UserService, private alertify: AlertifyService) {
    // userId = this.route.snapshot.params.id;
    // userId = route.params.subscribe(params => this.userId = params.id);
  }

  ngOnInit() {
    this.loadUserFormResolver();
    this.loadLookups();
  }

  loadUserFormResolver() {
    // user user resolver to get user before route insialized
    this.route.data.subscribe(data => {
      if (data.user) {
        this.user = data.user;
        this.galleryOptions = [{ imageAnimation: NgxGalleryAnimation.Slide }];
        this.galleryImages = this.user.photos.map(x => ({ small: x.url, medium: x.url, big: x.url }));
      } else { this.notExist = true; }
      // allow edit
      if (this.auth.currentUser.id === Number(this.route.snapshot.params.id)) {
        this.showEditBtn = true;
      }
    });
  }

  loadLookups() {
    this.lookupService.getLookup('gender').subscribe(res => this.genderLookup = res);
    this.lookupService.getLookup('city').subscribe(res => this.cityLookup = res);
    this.lookupService.getLookup('country').subscribe(res => this.countryLookup = res);
  }

  editUser() {
    this.userService.updateUser(this.user).subscribe(res => {
      if (res) {
        this.editMode = false;
        this.user = res;
        this.alertify.success('User Updated Successfully');
      }
    }, err => {
      this.alertify.error('unauthorized');
      this.editMode = false;
    });
  }

}
