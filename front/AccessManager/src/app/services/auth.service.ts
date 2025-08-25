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
      platform: navigator.platform,
      language: navigator.language,
    };

    const deviceName = `example device`;

    return this.http.post(
      `${this.apiUrl}/login`,
      { email, password, deviceName },
      { withCredentials: true }
    );
  }

  logout() {
    return this.http.post(
      `${this.apiUrl}/logout`,
      {},
      { withCredentials: true }
    );
  }

  refresh() {
    return this.http.post(
      `${this.apiUrl}/refresh`,
      {},
      { withCredentials: true }
    );
  }

  isAuthenticated() {
    return this.http.post(
      `${this.apiUrl}/isLoggedIn`,
      {},
      { withCredentials: true }
    );
  }
}
