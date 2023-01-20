import { Coefficient } from "./coefficient";
import { Faculty } from "./faculty";

export interface Speciality {
  specialityId: number;
  specialityName: string;
  specialityCode: number;

  facultyId: number;
  faculty: Faculty;

  educationalProgram: string;
  educationDegree: string;
  branchName: string;
  offerType: string;
  educationForm: string;
  educationCost: number;
  seatTotal: number;
  submittedApplicationsTotal: number;
  budgetTotal: number;
  contractTotal: number;
  quota1Total: number;
  quota2Total: number;

  coefficients: Coefficient[];
}
