import { Eie } from "./eie.model";

export interface Coefficient {
  coefficientId: number;
  specialityId: number;
  coefficientValue: number;
  eieId: number;
  eie: Eie;
}
