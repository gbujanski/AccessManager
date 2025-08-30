import { FormConfig } from "@frontend/models";
import { emailControlConfig } from "./single-control-config/email-control";
import { roleControlConfig } from "./single-control-config/role-control";

export const editUserFormConfig: FormConfig = {
  ...emailControlConfig,
  ...roleControlConfig,
}
