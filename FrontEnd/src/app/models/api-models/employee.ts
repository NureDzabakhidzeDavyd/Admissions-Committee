import { Faculty } from './faculty';
import { Person } from './person.model';
import { Working } from './working';

export interface Employee {
  employeeId: number;
  person: Person;
  faculty: Faculty;
  working: Working[];
  careerInfo: string;
}
