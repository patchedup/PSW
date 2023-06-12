import { User } from './User';

export class Blog {
  id: number = 0;
  title: string = '';
  content: string = '';
  doctor: User = new User();
}
