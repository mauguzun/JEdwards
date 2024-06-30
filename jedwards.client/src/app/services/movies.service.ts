import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../models/Movie';
import { MovieFullInfo } from '../models/MovieFullInfo';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {


  private apiUrl = 'https://localhost:7091/Movie/'; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  searchMovie(query: string): Observable<Movie[]> {
    return this.http.post<any>(`${this.apiUrl}search`, { query });
  }

  viewMovie(query: string) : Observable<MovieFullInfo> {
    return this.http.post<any>(`${this.apiUrl}`, { query });
  }
}


