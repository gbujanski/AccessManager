import { Injectable, Signal } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { map, merge, startWith } from 'rxjs';
import { toSignal } from '@angular/core/rxjs-interop';
import { ErrorsList, FormConfig } from '@frontend/models';

interface BuiltForm {
  formGroup: FormGroup;
  errors: Record<string, Signal<string>>;
}

@Injectable()
export class FormsBuilderService {
  buildForm(config: FormConfig): BuiltForm {
    const formGroup = new FormGroup({});
    const errors: Record<string, Signal<string>> = {}; 

    for (const [controlName, controlConfig] of Object.entries(config)) {
      const newControl = new FormControl('', controlConfig.validators);
      
      formGroup.addControl(controlName, newControl);
      errors[controlName] = this.trackErrorsAsSignal(newControl, controlConfig.errors);
    }

    return { formGroup, errors };
  }

  trackErrorsAsSignal(
    control: FormControl,
    errorsList: ErrorsList,
  ) {
    return toSignal(
      merge(
        control.valueChanges,
        control.statusChanges
      )
      .pipe(
        startWith(control.status),
        map(() => {
          if (!control.errors) {
            return '';
          }
          const firstErrorKey = Object.keys(control.errors)[0];
          return errorsList[firstErrorKey] ?? '';
        })
      ),
      { initialValue: '' }
    )
  }
}
