import { Eie } from './eie';

export interface Coefficient {
  coefficientId: number;
  specialityId: number;
  coefficientValue: number;
  eieId: number;
  eie: Eie;
}
