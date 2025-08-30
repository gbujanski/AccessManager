import { ChangeDetectionStrategy, Component, inject, Signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { FormsBuilderService } from '@frontend/services';
import { addUserFormConfig } from '@frontend/form-configs';

@Component({
  selector: 'lib-add-user-dialog',
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
  templateUrl: './add-user-dialog.html',
  styleUrl: './add-user-dialog.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [FormsBuilderService]
})

export class AddUserDialog {
  addUserForm: FormGroup;
  roles = ['Admin', 'User'];
  emailErrorMessage: Signal<string>;
  passwordErrorMessage: Signal<string>;
  roleErrorMessage: Signal<string>;

  private dialogRef = inject(MatDialogRef<AddUserDialog>);
  private fbs = inject(FormsBuilderService);

  constructor() {
    const {formGroup, errors} = this.fbs.buildForm(addUserFormConfig);

    this.addUserForm = formGroup;
    this.emailErrorMessage = errors['email'];
    this.passwordErrorMessage = errors['password'];
    this.roleErrorMessage = errors['role'];

  }

  onSubmit() {
    if (this.addUserForm.valid) {
      this.dialogRef.close(this.addUserForm.value);
    }
  }

  onCloseClick() {
    this.dialogRef.close();
  }
}
