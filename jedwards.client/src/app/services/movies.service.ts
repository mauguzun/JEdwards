import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../models/Movie';
import { MovieDetail } from '../models/MovieDetail';
import { environment } from '../../environments/environment.development';
import { SearchRequest } from '../models/SearchRequest';


@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  private apiUrl = `${environment.backendUrl}/Movie/`; 

  constructor(private http: HttpClient) { }

  searchMovie(query: string): Observable<Movie[]> {
    return this.http.post<any>(`${this.apiUrl}search`, new  SearchRequest(query));
  }

  viewMovie(query: string) : Observable<MovieDetail> {
    return this.http.post<any>(`${this.apiUrl}`, new  SearchRequest(query));
  }
}


