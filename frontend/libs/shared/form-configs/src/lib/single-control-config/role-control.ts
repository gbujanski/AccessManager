import { Validators } from "@angular/forms";
import { FormConfig } from "@frontend/models";

export const roleControlConfig: FormConfig = {
  role: {
    validators: [Validators.required],
    errors: {
      required: 'Role is required.'
    }
  }
}
