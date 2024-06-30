import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { SearchQuery } from '../../models/SearchQuery';
import { QueriesService } from '../../services/queries.service';

@Component({
  selector: 'app-view-queries',
  templateUrl: './view-queries.component.html',
  styleUrl: './view-queries.component.css'
})
export class ViewQueriesComponent  implements OnDestroy {

  search$: Subscription | undefined;
  queries : SearchQuery[] =[];
  loading = true;

  displayedColumns: string[] = ['id', 'date', 'query', 'errorMessage'];


  constructor(private _queryService: QueriesService, private _snackBar: MatSnackBar) {

    this.search$ = this._queryService.getQueries()
      .subscribe({
        next: (queries: SearchQuery[]) => {
          this.queries = queries;
          this.loading = false;
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



  ngOnDestroy() {
    this.search$?.unsubscribe();
  }
}
