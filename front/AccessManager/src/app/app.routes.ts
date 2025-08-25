import { Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { authGuard } from './guards/authGuard';

export const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [authGuard]
  },
  {
    path: 'auth',
    loadComponent: () => import('./features/auth/auth.component')
      .then(m => m.AuthComponent)
  },
];
