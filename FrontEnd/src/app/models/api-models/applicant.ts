import { Person } from './person';

export interface Applicant {
  applicantId: number;
  person: Person;
  certificate: number;
}
