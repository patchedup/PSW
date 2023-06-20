import { User } from "./User";

export class LoggedInUserDTO {
    token: string = '';
    user : User = new User();
  }
  