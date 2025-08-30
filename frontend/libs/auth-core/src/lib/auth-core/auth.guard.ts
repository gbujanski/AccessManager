import { CanActivateFn } from "@angular/router";
import { AuthService } from "./auth.service";
import { inject } from "@angular/core";
import { catchError, map, of } from "rxjs";

export const authGuard: CanActivateFn = () => {
  const authService = inject(AuthService);

  return authService.isAuthenticated().pipe(
    map(() => true),
    catchError(() => of(false))
  );
}
