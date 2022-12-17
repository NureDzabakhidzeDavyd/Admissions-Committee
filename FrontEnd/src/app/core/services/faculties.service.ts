import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Faculty } from 'src/app/models/api-models/faculty';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class FacultiesService extends DataService {
  faculty: Faculty = {
    facultyId: 0,
    facultyName: '',
  };

  faculties: Faculty[] = [];

  constructor(_httpClient: HttpClient) {
    super(_httpClient);
    this.entityType = 'faculties';
  }
}
