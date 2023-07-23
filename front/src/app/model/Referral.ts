import { Appointment } from './Appointement';
import { User } from './User';

export class Referral {
  id?: number = 0;
  isUsed: number = 0;
  forDoctorId: number = 0;
  appointments: Appointment[] = [];
  forDoctor: User | null = null;
}
