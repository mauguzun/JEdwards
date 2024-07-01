import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchQuery } from '../models/SearchQuery';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class QueriesService {

  private apiUrl = `${environment.backendUrl}/SearchQuery/`; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  getQueries(): Observable<SearchQuery[]> {
    return this.http.get<any>(`${this.apiUrl}`);
  }
}
