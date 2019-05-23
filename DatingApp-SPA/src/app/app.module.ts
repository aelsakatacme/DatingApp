// imports
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';

// modules
import { AppRoutingModule } from './app-routing.module';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';

// components
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberProfilePhotosComponent } from './members/member-profile-photos/member-profile-photos.component';

// guards
import { AuthGuard } from './_guards/auth.guard';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';

// resolvers
import { MemberProfileResolver } from './_resolvers/member-profile.resolver';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MembersListComponent,
    ListsComponent,
    MessagesComponent,
    MemberProfileComponent,
    MemberProfilePhotosComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token'),
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/auth'],
      }
    }),
    NgxGalleryModule,
    BsDropdownModule.forRoot({}),
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    NgSelectModule,
  ],
  providers: [
    AuthGuard,
    MemberProfileResolver,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
