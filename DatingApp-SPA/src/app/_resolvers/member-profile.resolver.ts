import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { UserDTO } from '../_models/UserDTO';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable()
export class MemberProfileResolver implements Resolve<UserDTO> {

 constructor(private userService: UserService, private router: Router, private alertify: AlertifyService) {
 }

 resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UserDTO> | Promise<UserDTO> | UserDTO {
  return this.userService.getUser(route.params.id).pipe(catchError(error => {
   this.alertify.error(error);
   this.router.navigate(['/members']);
   return of(null);
  }));
 }

}