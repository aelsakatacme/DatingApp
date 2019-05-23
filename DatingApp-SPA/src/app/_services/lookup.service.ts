import { Lookup } from './../_models/Lookup';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LookupService {

  private baseUrl = environment.apiUrl + 'lookup/';

  constructor(private http: HttpClient) { }

  getLookup(type: string): Observable<Lookup[]> {
    return this.http.get<Lookup[]>(this.baseUrl + type);
  }
}
