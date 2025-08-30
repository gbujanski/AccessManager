import { FormConfig } from "@frontend/models";
import { emailControlConfig } from "./single-control-config/email-control";
import { roleControlConfig } from "./single-control-config/role-control";
import { passwordControlConfig } from "./single-control-config/password-control";

export const addUserFormConfig: FormConfig = {
  ...emailControlConfig,
  ...roleControlConfig,
  ...passwordControlConfig,
}
