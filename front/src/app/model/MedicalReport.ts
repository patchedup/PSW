import { Appointment } from './Appointement';

export class MedicalReport {
  id: number = 0;
  appointment: Appointment = new Appointment();
  diagnosis: string = '';
  treatment: string = '';
}
