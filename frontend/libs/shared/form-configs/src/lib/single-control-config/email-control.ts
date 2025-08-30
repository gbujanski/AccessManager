import { Validators } from "@angular/forms";
import { FormConfig } from "@frontend/models";

export const emailControlConfig: FormConfig = {
  email: {
    validators: [Validators.required, Validators.email],
    errors: {
      required: 'Email is required.',
      email: 'Please enter a valid email address.'
    }
  }
}
