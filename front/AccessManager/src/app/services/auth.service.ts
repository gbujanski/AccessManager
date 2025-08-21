import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/auth';

  constructor(private http: HttpClient) { }

  login(email: string, password: string) {
    const browserInfo = {
      browser: navigator.userAgent,
      platform: navigator.platform,        // np. "Win32"
      language: navigator.language,        // np. "pl-PL"
    };

    // const deviceName = `${ browserInfo.browser } on ${ browserInfo.platform } with language ${ browserInfo.language }`;
    const deviceName = `example device`;
    return this.http.post(`${this.apiUrl}/login`, { email, password, deviceName }, { withCredentials: true });
  }

  logout() {
    return this.http.post(`${this.apiUrl}/logout`, {});
  }

  refresh() {
    return this.http.get(`${this.apiUrl}/refresh`);
  }
}
