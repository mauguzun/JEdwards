import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { Observable, catchError, map, throwError } from 'rxjs';
import { Movie } from '../models/Movie';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  private apiUrl = 'https://localhost:7091/Movie/'; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  searchMovie(query: string): Observable<Movie[]> {
    return this.http.post<any>(`${this.apiUrl}search`, { query });
  }
}


