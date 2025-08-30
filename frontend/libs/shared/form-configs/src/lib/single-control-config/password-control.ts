import { Validators } from "@angular/forms";
import { FormConfig } from "@frontend/models";

export const passwordControlConfig: FormConfig = {
  password: {
    validators: [Validators.required, Validators.minLength(6)],
    errors: {
      required: 'Password is required.',
      minlength: 'Password must be at least 6 characters long.'
    }
  }
}
