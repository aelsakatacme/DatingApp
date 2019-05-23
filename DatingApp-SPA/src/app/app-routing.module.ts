import { MemberProfileResolver } from './_resolvers/member-profile.resolver';
import { AuthGuard } from './_guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers : 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MembersListComponent },
      { path: 'member/:id', component: MemberProfileComponent, resolve: { user: MemberProfileResolver } },
      { path: 'messages', component: MessagesComponent },
      { path: 'lists', component: ListsComponent },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
