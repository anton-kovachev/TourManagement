import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { OpenIdConnectService } from './open-id-connect.service';

@Injectable()
export class RequireAuthenticatedUserGuardService implements CanActivate {

  constructor(private openIdConnectService: OpenIdConnectService, private router: Router) { }

  canActivate(): boolean {
    if (this.openIdConnectService.userAvailable) {
      return true;
    }

    this.openIdConnectService.triggerSignIn();
    return false;
  }
}
