// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  apiUrl: 'http://localhost:5000/api/',
  openIdConnectSettings: {
    authority: 'http://localhost:5010/',
    client_id: 'tourmanagementclient',
    redirect_uri: 'http://localhost:4200/signin-oidc',
    scope: 'openid profile roles tourmanagementapi',
    response_type: 'token id_token',
    post_logout_redirect_uri: 'http://localhost:4200/',
    automaticSilenRenew: true,
    silent_redirect_uri: 'https://localhost:4200/redirect-silentrenew'

  },
  production: false
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
