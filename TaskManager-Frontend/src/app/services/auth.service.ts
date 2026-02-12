import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7123/api/Auth/login';

  constructor(private http: HttpClient) {}

  isLoggedIn(): boolean {
    // Converts value to Boolean (true if token exists, false if null) and returns it.
    // This is a common way to check if a user is authenticated by verifying the presence of a token in localStorage.
    return !!localStorage.getItem('token');
  }

  login(credentials: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, credentials);
  }
}
