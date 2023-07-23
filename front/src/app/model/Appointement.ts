import { InternistData } from './InternistData';

export class Appointment {
  id: number = 0;
  doctorId: number = 0;
  patientId: number = 0;
  time: string = '';
  reportId?: number = 0;
  internistData: InternistData = new InternistData();
}
