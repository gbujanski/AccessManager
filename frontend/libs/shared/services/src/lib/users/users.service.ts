import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '@frontend/models';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private apiUrl = 'http://localhost:5000/api/users';
  private http: HttpClient = inject(HttpClient);

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(
      this.apiUrl,
      { withCredentials: true }
    );
  }

  getUser(id: string): Observable<User> {
    return this.http.get<User>(
      `${this.apiUrl}/${id}`,
      { withCredentials: true }
    );
  }

  addUser(user: Partial<User>): Observable<User> {
    return this.http.post<User>(
      this.apiUrl,
      user,
      { withCredentials: true }
    );
  }

  deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`,
      { withCredentials: true }
    );
  }

  editUser(id: string, user: Partial<User>): Observable<User> {
    return this.http.put<User>(
      `${this.apiUrl}/${id}`,
      user,
      { withCredentials: true }
    );
  }
}
