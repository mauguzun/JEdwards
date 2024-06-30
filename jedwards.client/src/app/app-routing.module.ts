import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './pages/movies/search/search.component';
import { ViewQueriesComponent } from './pages/view-queries/view-queries.component';
import { ViewMovieComponent } from './pages/movies/viewmovie/viewmovie.component';



const routes: Routes = [
  {
    path: "",
    component: SearchComponent,
  },
  {
    path: 'view-queries',
    component: ViewQueriesComponent
  }, {
    path: 'view-movie/:imdbID', // Define a route parameter ':id'
    component: ViewMovieComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
