import { FormConfig } from "@frontend/models";
import { emailControlConfig } from "./single-control-config/email-control";
import { passwordControlConfig } from "./single-control-config/password-control";

export const authUserFormConfig: FormConfig = {
  ...emailControlConfig,
  ...passwordControlConfig,
}
