import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user.model';
import { Observable } from 'rxjs';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog } from '@angular/material/dialog';
import { AddUserDialogComponent } from '../../shared/add-user-dialog/add-user-dialog.component';
import { EditUserDialogComponent } from '../../shared/edit-user-dialog/edit-user-dialog.component';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-dashboard',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  users$: Observable<User[]> = new Observable<User[]>();
  isLoading = true;
  displayedColumns: string[] = ['id', 'email', 'role', 'actions'];
  dialog = inject(MatDialog);

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.users$ = this.usersService.getUsers();
  }

  openAddUserDialog() {
    this.dialog.open(AddUserDialogComponent).afterClosed().subscribe(result => {
      if (result) {
        this.usersService.addUser(result).subscribe(() => {
          this.users$ = this.usersService.getUsers();
        });
      }
    });
  }

  openEditUserDialog(user: User) {
    this.dialog.open(EditUserDialogComponent, {
      data: { id: user.id, email: user.email, role: user.role }
    }).afterClosed().subscribe(result => {
      if (result) {
        this.usersService.editUser(user.id, result).subscribe(() => {
          this.users$ = this.usersService.getUsers();
        });
      }
    });
  }

  removeUser(id: string) {
    this.dialog.open(ConfirmDialogComponent, {
    }).afterClosed().subscribe(confirm => {
      if (confirm) {
        this.usersService.deleteUser(id).subscribe(() => {
          this.users$ = this.usersService.getUsers();
        });
      }
    });
  }
}
