import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private apiUrl = 'https://localhost:7123/api/Tasks';

  constructor(private http: HttpClient) {}

  // Get all tasks for the authenticated user.
  getTasks(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Add a new task for the authenticated user.
  addTask(task: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, task);
  }

  // Mark a task as completed by its ID.
  completeTask(id: number): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}/complete`, {});
  }

  // Delete a task by its ID (soft delete).
  deleteTask(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
