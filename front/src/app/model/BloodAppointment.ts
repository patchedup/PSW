import { InternistData } from './InternistData';

export class BloodAppointment {
  id: number = 0;
  hospitalName: string = '';
  isArchived: boolean = false;
  shouldPublish: boolean = true;
  time: string = '';
  patientId: number = 0;
}
