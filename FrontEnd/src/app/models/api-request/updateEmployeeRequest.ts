import { UpdatePersonRequest } from "./updatePersonRequest";

export interface UpdateEmployeeRequest {
  person: UpdatePersonRequest;
  FacultyId: number;
  CareerInfo: string;
}
