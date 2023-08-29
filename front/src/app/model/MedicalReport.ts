import { Appointment } from './Appointement';
import { InternistData } from './InternistData';

class ReportAppointment {
  id: number = 0;
  doctorId: number = 0;
  patientId: number = 0;
  time: string = '';
  reportId?: number = 0;
  measuredInternistData: InternistData = new InternistData();
}

export class MedicalReport {
  id: number = 0;
  appointment: Appointment = new Appointment();
  diagnosis: string = '';
  treatment: string = '';
  appointments?: ReportAppointment[] = [];
}
