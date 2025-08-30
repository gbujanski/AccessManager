import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/auth';
  private http = inject(HttpClient);

  login(email: string, password: string) {
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
