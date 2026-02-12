import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/login.component';
import { TasksComponent } from './features/tasks/tasks.component';
import { authGuard } from '././core/guards/auth-guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent }, // Public route for the login page, accessible to all users.
  { path: 'tasks', component: TasksComponent, canActivate: [authGuard] }, // Protects the /tasks route with the authGuard, ensuring only authenticated users can access it.
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redirects the root path to /login, making the login page the default landing page for the application.
];
