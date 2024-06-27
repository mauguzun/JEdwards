import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../models/Movie';

@Injectable({
  providedIn: 'root'
})
export class QueriesService {

  private apiUrl = 'https://localhost:7091/Movie/'; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  searchMovie(title: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/search`,  { title: 'shadow' });
  }
}
