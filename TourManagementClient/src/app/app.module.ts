import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { } from 'automapper-ts';

import { AppComponent } from './app.component';
import { ToursComponent } from './tours/tours.component';
import { TourService } from './tours/shared/tour.service';
import { BaseService } from './shared/base.service';
import { HttpClient } from '@angular/common/http';
import { AboutComponent } from './about/about.component';
import { AppRoutingModule } from './app-routing.module';
import { TourUpdateComponent } from './tours/tour-update/tour-update.component';
import { TourAddComponent } from './tours/tour-add/tour-add.component';
import { TourDetailComponent } from './tours/tour-detail/tour-detail.component';
import { ShowSingleComponent } from './tours/shows/show-single/show-single.component';
import { SigninOidcComponent } from './signin-oidc/signin-oidc.component';
import { OpenIdConnectService } from './shared/open-id-connect.service';
import { RequireAuthenticatedUserGuardService } from './shared/require-authenticated-user-guard.service';
import { AddAuthorizationHeaderInterceptor } from './shared/add-authorization-header-interceptor';
import { AddAcceptHeaderInterceptor } from './shared/add-accept-header-interceptor';
import { ShowsComponent } from './tours/shows/shows.component';
import { RedirectSilentRenewComponent } from './redirect-silent-renew/redirect-silent-renew.component';
import { HandleHttpErrorInterceptor } from './shared/handle-http-error-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    ToursComponent,
    AboutComponent,
    TourUpdateComponent,
    TourAddComponent,
    TourDetailComponent,
    ShowSingleComponent,
    ShowsComponent,
    SigninOidcComponent,
    RedirectSilentRenewComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS,
      useClass: AddAuthorizationHeaderInterceptor,
      multi: true },
    { provide: HTTP_INTERCEPTORS,
      useClass: AddAcceptHeaderInterceptor,
      multi: true },
    { provide: HTTP_INTERCEPTORS,
      useClass: HandleHttpErrorInterceptor,
      multi: true },
    OpenIdConnectService,
    OpenIdConnectService,
    RequireAuthenticatedUserGuardService],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {
    automapper
      .createMap('TourFormModel', 'TourWithManagerAndShowsForCreation')
      .forSourceMember('band', function (opts) { opts.ignore(); })
      .forSourceMember('manager', function (opts) { opts.ignore(); })
      .forMember('bandId', function (opts) { opts.mapFrom('band'); })
      .forMember('managerId', function (opts) { opts.mapFrom('manager'); });

    automapper
      .createMap('TourFormModel', 'TourWithManagerForCreation')
      .forSourceMember('band', function (opts) { opts.ignore(); })
      .forSourceMember('manager', function (opts) { opts.ignore(); })
      .forMember('bandId', function (opts) { opts.mapFrom('band'); })
      .forMember('managerId', function (opts) { opts.mapFrom('manager'); });

    automapper
      .createMap('TourFormModel', 'TourWithShowsForCreation')
      .forSourceMember('band', function (opts) { opts.ignore(); })
      .forSourceMember('manager', function (opts) { opts.ignore(); })
      .forMember('bandId', function (opts) { opts.mapFrom('band'); });

    automapper.createMap('TourFormModel', 'TourForUpdate');
    automapper.createMap('TourFormModel', 'TourWithShowsForUpdate');

    automapper.createMap('TourFormModel', 'TourWithEstimatedProfitsAndManagerForUpdate')
      .forSourceMember('manager', function (opts) { opts.ignore(); })
      .forMember('managerId', function (opts) { opts.mapFrom('manager'); });

    automapper.createMap('TourFormModel', 'TourWithEstimatedProfitsAndManagerAndShowsForUpdate')
      .forSourceMember('manager', function (opts) { opts.ignore(); })
      .forMember('managerId', function (opts) { opts.mapFrom('manager'); });

    automapper.createMap('ShowFormModel', 'ShowForUpdate');
  }
}
