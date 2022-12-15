import { Rank } from './rank.model';

export interface Working {
  workingId: number;
  employeeId: number;
  rank: Rank;
  issuedYear: number;
}
