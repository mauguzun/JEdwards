import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../models/Movie';
import { SearchQuery } from '../models/SearchQuery';

@Injectable({
  providedIn: 'root'
})
export class QueriesService {

  private apiUrl = 'https://localhost:7091/SearchQuery/'; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  getQueries(): Observable<SearchQuery[]> {
    return this.http.get<any>(`${this.apiUrl}`);
  }
}
