import { Routes, RouterModule, CanActivate, CanDeactivate } from '@angular/router';


import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToursComponent } from './tours/tours.component';
import { AboutComponent } from './about/about.component';
import { AppComponent } from './app.component';
import { TourDetailComponent } from './tours/tour-detail/tour-detail.component';
import { TourUpdateComponent } from './tours/tour-update/tour-update.component';
import { TourAddComponent } from './tours/tour-add/tour-add.component';
import { RequireAuthenticatedUserGuardService } from './shared/require-authenticated-user-guard.service';
import { SigninOidcComponent } from './signin-oidc/signin-oidc.component';
import { RedirectSilentRenewComponent } from './redirect-silent-renew/redirect-silent-renew.component';
import { ConfirmExitOnUnsubmittedFormService } from './shared/confirm-exit-on-unsubmitted-form.service';

const routes: Routes = [
  { path: '', redirectTo: 'tours', pathMatch: 'full', canActivate: [RequireAuthenticatedUserGuardService] },
  { path: 'tours', component: ToursComponent, canActivate: [RequireAuthenticatedUserGuardService]},
  { path: 'tours/:tourId', component: TourDetailComponent, canActivate: [RequireAuthenticatedUserGuardService] },
  { path: 'tour-update/:tourId', component: TourUpdateComponent, canActivate: [RequireAuthenticatedUserGuardService],
    canDeactivate: [ConfirmExitOnUnsubmittedFormService] },
  { path: 'tour-add', component: TourAddComponent, canActivate: [RequireAuthenticatedUserGuardService] },
  { path: 'about', component: AboutComponent },
  { path: 'signin-oidc', component: SigninOidcComponent },
  { path: 'redirect-silentrenew', component: RedirectSilentRenewComponent },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
