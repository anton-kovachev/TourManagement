import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

import { Band } from './band.model';
import { Manager } from './manager.model';


@Injectable({
  providedIn: 'root'
})
export class MasterDataService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getBands(): Observable<Band[]> {
    return this.http.get<Band[]>(`${this.apiUrl}bands`);
  }

  getManagers(): Observable<Manager[]> {
    return this.http.get<Manager[]>(`${this.apiUrl}managers`);
  }
}
