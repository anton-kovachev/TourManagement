import { Injectable } from '@angular/core';
import { User, UserManager  } from 'oidc-client';
import { environment } from '../../environments/environment';
import { ReplaySubject } from 'rxjs';

@Injectable()
export class OpenIdConnectService {

  private currentUser: User;
  private userManager = new UserManager(environment.openIdConnectSettings);

  userLoaded$ = new ReplaySubject<boolean>(1);

  constructor() {
    this.userManager.clearStaleState();

    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log('User loaded');
      }

      this.currentUser = user;
      this.userLoaded$.next(true);
    });

    this.userManager.events.addUserUnloaded(user => {
      if (!environment.production) {
        console.log('User unloaded');
      }

      this.currentUser = null;
      this.userLoaded$.next(false);
    });

  }

  triggerSignIn() {
    this.userManager.signinRedirect().then(function() {
      if (!environment.production) {
        console.log('Redirection to sign in triggered!');
      }
    });
  }

  handleCallBack() {
    this.userManager.signinRedirectCallback().then(user => {
      if (!environment.production) {
        console.log('Callback after user signin handled.', user);
      }
    });
  }

  handleSilentCallback() {
   this.userManager.signinSilentCallback().then( _ => {
    console.log('Token refreshed');
   });
  }

  triggerSignOut() {
    this.userManager.signoutRedirect().then(function(resp) {
      if (!environment.production) {
        console.log('Redirection to sign out triggred!', resp);
      }
    });
  }

  get userAvailable(): boolean {
    return this.currentUser != null;
  }

  get user(): User {
    return this.currentUser;
  }

  get isAdmin(): boolean {
    return this.user.profile.role === 'Admin';
  }
}
