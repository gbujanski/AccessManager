import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { User } from '@frontend/models';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog } from '@angular/material/dialog';
import { UsersService } from '@frontend/services';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'lib-users-dashboard',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './users-dashboard.html',
  styleUrl: './users-dashboard.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class UsersDashboard implements OnInit {
  users = signal<User[]>([]);
  displayedColumns: string[] = ['id', 'email', 'role', 'actions'];
  dialog = inject(MatDialog);
  usersService = inject(UsersService);
  
  ngOnInit(): void {
    this.refreshUsers();
  }

  private refreshUsers() {
    this.usersService.getUsers()
    .pipe(takeUntilDestroyed())
    .subscribe(users => {
      this.users.set(users);
    });
  }

  async openAddUserDialog() {
    const { AddUserDialog } = await import('@frontend/layout-dialogs');

    this.dialog.open(AddUserDialog).afterClosed().subscribe(result => {
      if (result) {
        this.usersService.addUser(result)
        .pipe(takeUntilDestroyed())
        .subscribe(() => {
          this.refreshUsers();
        });
      }
    });
  }

  async openEditUserDialog(user: User) {
    const { EditUserDialog } = await import('@frontend/layout-dialogs');

    this.dialog.open(EditUserDialog, {
      data: { id: user.id, email: user.email, role: user.role }
    }).afterClosed().subscribe(result => {
      if (result) {
        this.usersService.editUser(user.id, result)
        .pipe(takeUntilDestroyed())
        .subscribe(() => {
          this.refreshUsers();
        });
      }
    });
  }

  async removeUser(id: string) {
    const { ConfirmDialog } = await import('@frontend/layout-dialogs');

    this.dialog.open(ConfirmDialog, {
    }).afterClosed().subscribe(confirm => {
      if (confirm) {
        this.usersService.deleteUser(id)
        .pipe(takeUntilDestroyed())
        .subscribe(() => {
          this.refreshUsers();
        });
      }
    });
  }
}
