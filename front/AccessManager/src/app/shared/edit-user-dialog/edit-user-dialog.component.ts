import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-edit-user-dialog',
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule
  ],
  templateUrl: './edit-user-dialog.component.html',
  styleUrl: './edit-user-dialog.component.scss'
})
export class EditUserDialogComponent {
  editUserForm: FormGroup;

  roles = ['Admin', 'User'];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: {id: string, email: string, role: string},
    private usersService: UsersService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EditUserDialogComponent>
  ) {
    this.editUserForm = this.fb.group({
      email: [this.data.email, [Validators.required, Validators.email]],
      role: [this.data.role, Validators.required],
    });
  }

  ngOnInit() {
    this.usersService.getUser(this.data.id).subscribe(user => {
      this.editUserForm.patchValue(user);
    });
  }

  onSubmit() {
    if (this.editUserForm.valid) {
      this.dialogRef.close(this.editUserForm.value);
    }
  }

  onCloseClick() {
    this.dialogRef.close();
  }
}
