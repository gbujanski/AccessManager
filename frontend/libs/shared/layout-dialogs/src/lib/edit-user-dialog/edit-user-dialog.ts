import { ChangeDetectionStrategy, Component, inject, OnInit, Signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
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
import { FormsBuilderService, UsersService } from '@frontend/services';
import { editUserFormConfig } from '@frontend/form-configs';

@Component({
  selector: 'lib-edit-user-dialog',
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
  templateUrl: './edit-user-dialog.html',
  styleUrl: './edit-user-dialog.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [FormsBuilderService]
})

export class EditUserDialog implements OnInit {
  editUserForm: FormGroup;
  roles = ['Admin', 'User'];
  emailErrorMessage: Signal<string>;
  roleErrorMessage: Signal<string>;

  private usersService: UsersService = inject(UsersService);
  private dialogRef: MatDialogRef<EditUserDialog> = inject(MatDialogRef);
  private data = inject(MAT_DIALOG_DATA) as {id: string, email: string, role: string};
  private fbs = inject(FormsBuilderService);

  constructor() {
    const {formGroup, errors} = this.fbs.buildForm(editUserFormConfig);

    this.editUserForm = formGroup;
    this.emailErrorMessage = errors['email'];
    this.roleErrorMessage = errors['role'];
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
