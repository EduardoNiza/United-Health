import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class DashboardService {
    constructor(private http: HttpClient) {}

    public getLastDiet(): Observable<any> {
        const url = `${environment.apiNutritionUrl}/Nutritional/GetLastPrescription`;
        return this.http.get<any>(url)
          .pipe(
            catchError((error) => {
              return throwError(() => new Error('Error getting last diet'));
            })
          );
      }

      public getLastWorkout(): Observable<any> {
        const url = `${environment.apiTrainingUrl}/Training/GetLastPrescription`;
        return this.http.get<any>(url)
          .pipe(
            catchError((error) => {
              return throwError(() => new Error('Error getting last workout'));
            })
          );
      }
}
