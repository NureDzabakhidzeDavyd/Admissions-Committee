import { Faculty } from './faculty.model';
import { Person } from './person.model';
import { Working } from './working.model';

export interface Employee {
  employeeId: number;
  person: Person;
  faculty: Faculty;
  working: Working[];
  careerInfo: string;
}
