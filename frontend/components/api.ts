import { loginRequest } from '@/app/authConfig';
import { msalInstance } from '@/app/layout';
import axios from 'axios';

const instance = axios.create({
  baseURL: 'http://localhost:5156/api/',
  timeout: 1000,
  withCredentials: true
});

instance.interceptors.request.use(async (config) => {
  const account = msalInstance.getAllAccounts()[0];
  if (account) {
    const accessTokenResponse = await msalInstance.acquireTokenSilent({
      scopes: loginRequest.scopes,
      account: account
    });

    if (accessTokenResponse) {
      const accessToken = accessTokenResponse.accessToken;
      if (config.headers && accessToken) {
        config.headers['Authorization'] = 'Bearer ' + accessToken;
      }
    }
  }

  return config;
});

export default instance;
