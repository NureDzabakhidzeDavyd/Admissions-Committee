import { Person } from './person.model';

export interface Applicant {
  applicantId: number;
  person: Person;
  certificate: number;
}
