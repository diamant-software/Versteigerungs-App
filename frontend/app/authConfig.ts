/*
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License.
 */

import { Configuration, LogLevel } from '@azure/msal-browser';
const tenantName = "versteigerungen";

const b2cPolicies = {
  names: {
      signUpSignIn: "B2C_1_flow1",
      forgotPassword: "B2C_1_flow3",
      editProfile: "B2C_1_flow2"
  },
  authorities: {
      signUpSignIn: {
          authority: 'https://versteigerungen.b2clogin.com/versteigerungen.onmicrosoft.com/B2C_1_flow1',
          
      },
      forgotPassword: {
          authority: `https://versteigerungen.b2clogin.com/versteigerungen.onmicrosoft.com/B2C_1_flow3`,
      },
      editProfile: {
          authority: `https://versteigerungen.b2clogin.com/versteigerungen.onmicrosoft.com/B2C_1_flow2`
      }
  },
  authorityDomain: `${tenantName}.b2clogin.com`
}

/**
 * Configuration object to be passed to MSAL instance on creation.
 * For a full list of MSAL.js configuration parameters, visit:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md
 */
export const msalConfig: Configuration = {
  auth: {
    clientId: 'a4ace1db-c3e2-4a5f-9c87-d22e57abf21c',
    authority: b2cPolicies.authorities.signUpSignIn.authority,
    redirectUri: '/',
    knownAuthorities: [b2cPolicies.authorityDomain], 
    navigateToLoginRequestUrl: false,
    postLogoutRedirectUri: "/"
  },
  cache: {
    cacheLocation: 'sessionStorage', // This configures where your cache will be stored
    storeAuthStateInCookie: false // Set this to "true" if you are having issues on IE11 or Edge
  },
  system: {
    allowNativeBroker: false, // Disables WAM Broker
    loggerOptions: {
      loggerCallback: (level, message, containsPii) => {
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
        }
      }
    }
  }
};

/**
 * Scopes you add here will be prompted for user consent during sign-in.
 * By default, MSAL.js will add OIDC scopes (openid, profile, email) to any login request.
 * For more information about OIDC scopes, visit:
 * https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-permissions-and-consent#openid-connect-scopes
 */
export const loginRequest = {
  scopes: ['https://versteigerungen.onmicrosoft.com/Versteigerungs-App/unrestricted']
};

/**
 * Add here the scopes to request when obtaining an access token for MS Graph API. For more information, see:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/resources-and-scopes.md
 */
export const graphConfig = {
  graphMeEndpoint: 'https://graph.microsoft.com/v1.0/me'
};


