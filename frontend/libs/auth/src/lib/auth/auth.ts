import { ChangeDetectionStrategy, Component, inject, Signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '@frontend/auth-core';
import { FormsBuilderService } from '@frontend/services';
import { authUserFormConfig } from '@frontend/form-configs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'lib-auth',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './auth.html',
  styleUrl: './auth.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [FormsBuilderService]
})

export class Auth {
  authForm: FormGroup;
  emailErrorMsg: Signal<string>;
  passwordErrorMsg: Signal<string>;

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private fbs = inject(FormsBuilderService);

  constructor() {
    const {formGroup, errors} = this.fbs.buildForm(authUserFormConfig);

    this.authForm = formGroup;
    this.emailErrorMsg = errors['email'];
    this.passwordErrorMsg = errors['password'];

    this.authForm = this.fb.group({
      email: this.fb.control('', { validators: [Validators.required, Validators.email], nonNullable: true }),
      password: this.fb.control('', { validators: [Validators.required, Validators.minLength(6)], nonNullable: true })
    });
  }

  onSubmit() {
    if (this.authForm.valid) {
      const { email, password } = this.authForm.getRawValue();

      this.authService.login(email, password)
      .pipe(takeUntilDestroyed())
      .subscribe();
    }
  }
}
