import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DynamicFilters } from 'src/app/models/api-request/dynamic-filter/dynamicFilters';
import { environment } from 'src/enviroments/environment';
import { getParams } from '../arrays/dynamic-filters';

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

  public getAllByDynamicFilters(
    dynamicfilters: DynamicFilters
  ): Observable<any> {
    let queryParams = getParams(dynamicfilters.filters);

    return this._httpClient.get(`${this.getUrl()}?${queryParams.toString()}`);
  }

  public getById(id: number): Observable<any> {
    return this._httpClient.get(`${this.getUrl()}/${id}`);
  }

  public deleteById(id: number) {
    return this._httpClient.delete(`${this.getUrl()}/${id}`);
  }

  // private getParams(query: any) {
  //   let params: HttpParams = new HttpParams();
  //   for (const key of Object.keys(query)) {
  //     if (query[key]) {
  //       if (query[key] instanceof Array) {
  //         query[key].forEach((item: any) => {
  //           params = params.append(`${key.toString()}[]`, item);
  //         });
  //       } else {
  //         params = params.append(key.toString(), query[key]);
  //       }
  //     }
  //   }
  //   return params;
  // }

  constructor(protected readonly _httpClient: HttpClient) {}
}
