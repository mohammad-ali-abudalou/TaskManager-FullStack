import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskItem } from '../../core/models/task.model';
import { TimeagoModule } from 'ngx-timeago';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, FormsModule, TimeagoModule],
  templateUrl: './tasks.component.html',
})
export class TasksComponent implements OnInit {
  tasks: TaskItem[] = [];
  filteredTasks: TaskItem[] = [];
  searchTerm: string = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.loadTasks();
  }

  newTask: TaskItem = {
    title: '',
    description: '',
    isCompleted: false,
    isDeleted: false,
    createdAt: new Date(),
    userId: '',
  };

  addTask() {
    if (!this.newTask.title || !this.newTask.description) {
      alert('Please enter a title and description of the task.');
      return;
    }

    this.http.post('https://localhost:7123/api/Tasks', this.newTask).subscribe({
      next: (newlyCreatedTask: any) => {
        // Use unshift instead of push to make the new task appear first immediately.
        this.tasks.unshift(newlyCreatedTask);
        this.filterTasks(); // Update the filtered list.
        this.newTask = {
          title: '',
          description: '',
          isCompleted: false,
          isDeleted: false,
          createdAt: new Date(),
          userId: '',
        };

        this.cdr.detectChanges();
      },
    });
  }

  // Load tasks from the backend API and handle the response.
  loadTasks() {
    this.http.get<any[]>('https://localhost:7123/api/Tasks').subscribe({
      next: (data) => {
        this.tasks = data;

        // It is important that the tasks appear when the page is opened without the need to type in the search box.
        // By setting filteredTasks to data, we ensure that all tasks are displayed when the page loads, and then the user can filter them as needed.
        this.filteredTasks = data;

        // After updating the tasks and filteredTasks, we need to trigger change detection to update the view.
        this.cdr.detectChanges();
      },
      error: (err) => alert('Failed to load tasks: ' + err.message),
    });
  }

  deleteTask(id: number) {
    this.http.delete(`https://localhost:7123/api/Tasks/${id}`).subscribe(() => {
      // Instead of deleting, we find the task and change its flag.
      const task = this.tasks.find((t) => t.id === id);
      if (task) {
        task.isDeleted = true;
        this.filterTasks(); // To update the interface immediately without waiting for the next load.

        this.cdr.detectChanges(); // Trigger change detection to update the view immediately after marking the task as deleted.
      }
    });
  }

  // State change function (Toggle).
  toggleComplete(task: any) {
    task.isCompleted = !task.isCompleted;
    this.http.put(`https://localhost:7123/api/Tasks/${task.id}`, task).subscribe();
  }

  // Filter function to search for tasks based on the title.
  filterTasks() {
    this.filteredTasks = this.tasks.filter((t) =>
      t.title.toLowerCase().includes(this.searchTerm.toLowerCase()),
    );
  }

  // Logout function to clear the token and navigate back to the login page.
  logout() {
    localStorage.removeItem('token'); // Remove the token from the browser's memory to log out the user.
    this.router.navigate(['/login']); // Return to the beginning (login page).
  }
}
