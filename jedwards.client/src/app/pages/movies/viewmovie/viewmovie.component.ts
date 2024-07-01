import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { MoviesService } from '../../../services/movies.service';
import { MovieFullInfo } from '../../../models/MovieFullInfo';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view-movie',
  templateUrl: './viewmovie.component.html',
  styleUrl: './viewmovie.component.css'
})
export class ViewMovieComponent implements OnDestroy, OnInit {

  id: string = '';
  search$: Subscription | undefined;
  movie: MovieFullInfo | undefined;
  loading = false;

  constructor(private _movieService: MoviesService, private _snackBar: MatSnackBar, private _route: ActivatedRoute) {}

  ngOnInit(): void {

    this.loading = true;
    this.movie = undefined;

    const imdbID = this._route.snapshot.paramMap.get('imdbID');
    if(!imdbID){
      this.loading = true;
      this._snackBar.open(`moview wiht that id not exist ${imdbID}`)
    }else{
      this.search$ = this._movieService.viewMovie(imdbID).subscribe({
        next: (movies: MovieFullInfo) => {
          this.movie = movies;
        },
        error: (error: HttpErrorResponse) => {
          this._snackBar.open(error.error === Object && 'title' ? error.error['title'] : error.error);
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
        }
      });
    }
   
  }

  ngOnDestroy() {
    this.search$?.unsubscribe();
  }
}

