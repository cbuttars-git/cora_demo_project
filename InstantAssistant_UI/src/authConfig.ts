import { LogLevel, PublicClientApplication } from '@azure/msal-browser';

// Config object to be passed to Msal on creation
export const msalConfig = {
  auth: {
    clientId: 'd6972a64-bcbb-4df8-b60b-e18a78831a70',
    authority: 'https://login.microsoftonline.com/dd58e0fd-36c8-4ed5-879a-784b40e72dc6',
    redirectUri: 'http://localhost:8081/', // Must be registered as a SPA redirectURI on your app registration
    postLogoutRedirectUri: 'http://localhost:8081/' // Must be registered as a SPA redirectURI on your app registration
  },
  cache: {
    cacheLocation: 'localStorage'
  },
  system: {
      loggerOptions: {
          loggerCallback: (level: LogLevel, message: string, containsPii: boolean) => {
              if (containsPii) {	
                  return;	
              }
              switch (level) {	
                  case LogLevel.Error:	
                      console.error(message);	
                      return;	
                  case LogLevel.Info:	
                      console.info(message);	
                      return;	
                  case LogLevel.Verbose:	
                      console.debug(message);	
                      return;	
                  case LogLevel.Warning:	
                      console.warn(message);	
                      return;	
                  default:
                      return;
              }
          },
          logLevel: LogLevel.Verbose
      }
  }
};

export const msalInstance = new PublicClientApplication(msalConfig);

// Add here scopes for id token to be used at MS Identity Platform endpoints.
export const loginRequest = {
  scopes: ['User.Read'],
};

// Add here the endpoints for MS Graph API services you would like to use.
export const graphConfig = {
  graphMeEndpoint: 'https://graph.microsoft.com/v1.0/me',
};