import { PhotoDTO } from './../../_models/PhotoDTO';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-member-profile-photos',
  templateUrl: './member-profile-photos.component.html',
  styleUrls: ['./member-profile-photos.component.scss']
})
export class MemberProfilePhotosComponent implements OnInit {
  @Input() photos: PhotoDTO[];

  constructor() { }

  ngOnInit() {
  }

}
