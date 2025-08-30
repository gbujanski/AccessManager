import { ValidatorFn } from "@angular/forms";
import { ErrorsList } from "./errors-list";

export interface FormControlConfig {
  validators: ValidatorFn[];
  errors: ErrorsList;
}