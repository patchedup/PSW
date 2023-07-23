export class User {
  id: number = 0;
  email: string = '';
  password: string = '';
  role: string = 'PATIENT';
  firstName: string = '';
  lastName: string = '';
  numberOfPenalties: number = 0;
  isBlocked: boolean = false;
  specialization: string = '';
  is_female: number = 0;
  assignedGeneralPracticeDoctorId: number = 0;
}
