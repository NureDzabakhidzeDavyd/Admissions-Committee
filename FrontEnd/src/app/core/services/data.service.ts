import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/enviroments/environment';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  public entityType = '';

  public getUrl(): string {
    return environment.apiUrl + this.entityType;
  }

  public getAll(): Observable<any> {
    return this._httpClient.get(this.getUrl());
  }

  public getById(id: number): Observable<any> {
    return this._httpClient.get(`${this.getUrl()}/${id}`);
  }

  constructor(protected readonly _httpClient: HttpClient) {}
}
