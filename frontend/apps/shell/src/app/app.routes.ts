import { Route } from '@angular/router';
import { authGuard } from '@frontend/auth-core';

export const appRoutes: Route[] = [
    {
        path: 'auth',
        loadComponent: () => import('@frontend/auth').then(m => m.Auth)
    },
    {
        path: 'users',
        loadComponent: () => import('@frontend/users').then(m => m.UsersDashboard),
        canActivate: [authGuard]
    }
];
