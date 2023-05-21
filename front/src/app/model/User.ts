export class User {
  id: number = 0;
  email: string = '';
  password: string = '';
  role: string = '';
  firstName: string = '';
  lastName: string = '';
  numberOfPenalties: number = 0;
  isBlocked: boolean = false;
  specialization: string = '';
  assignedGeneralPracticeId: number = 0;
}
