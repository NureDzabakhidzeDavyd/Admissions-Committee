import { Rank } from './rank';

export interface Working {
  workingId: number;
  employeeId: number;
  rank: Rank;
  issuedYear: number;
}
