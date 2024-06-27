import { Component, OnDestroy } from '@angular/core';
import { MoviesService } from '../../services/movies.service';
import { Subscription } from 'rxjs';
import { Movie } from '../../models/Movie';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnDestroy {

  title: string = '';
  search$ : Subscription | undefined ;
  movies :Movie[] |undefined;
  loading = false;

  constructor(private _movieService: MoviesService,private _snackBar: MatSnackBar) {}

  searchMovies() {
    this.loading = true;
    this.movies = undefined;
    
    this.search$ =  this._movieService.searchMovie(this.title.trim()).subscribe({
      next: (movies: Movie[]) => {
        this.movies = movies;
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        this._snackBar.open(error.error);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  ngOnDestroy() {
      this.search$?.unsubscribe();
  }
}
