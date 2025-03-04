import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class NutritionService {
    constructor(private http: HttpClient) {}

    public getAllNutritionists() {
      return this.http.get<any>(`${environment.apiNutritionUrl}/Nutritional/GetAllNutritionists`).pipe(
        catchError((_: HttpErrorResponse, __: Observable<any>) => {
          return of({});
        }));
    }

    insertFreeTimeSlot(date: string): Observable<{ message: string }> {
      const params = new HttpParams().set('date', date);
      const url = `${environment.apiNutritionUrl}/Nutritional/InsertFreeTimeSlot`;
      return this.http.post<{ message: string }>(url, null, { params })
        .pipe(
          catchError((error) => {
            return throwError(() => new Error('Error inserting free time slot'));
          })
        );
    }

    public insertConsultation(date: string, nutritionistId: string): Observable<{ message: string }> {
      const params = new HttpParams().set('date', date).set('nutritionistId', nutritionistId);
      const url = `${environment.apiNutritionUrl}/Nutritional/InsertConsultation`;
      return this.http.post<{ message: string }>(url, null, { params })
        .pipe(
          catchError((error) => {
            return throwError(() => new Error('Erro ao inserir consulta'));
          })
        );
    }

    public getAllConsultations(): Observable<any> {
      return this.http.get<any>(`${environment.apiNutritionUrl}/Nutritional/GetAllConsultation`).pipe(
          catchError((_: HttpErrorResponse, __: Observable<any>) => {
              return of([]);
          })
      );
  }

  public insertNutritionPrescription(appointmentId: number, prescription: string): Observable<{ message: string }> {
    const params = new HttpParams().set('appointmentId', appointmentId.toString()).set('prescription', prescription);
    const url = `${environment.apiNutritionUrl}/Nutritional/InsertNutritionPrescription`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error inserting nutritional prescription'));
        })
      );
  }

  public updateConsultationStatus(appointmentId: number): Observable<{ message: string }> {
    const params = new HttpParams().set('appointmentId', appointmentId.toString());
    const url = `${environment.apiNutritionUrl}/Nutritional/CancelConsultation`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error updating consultation status'));
        })
      );
  }

  public getAllPatients(): Observable<any> {
    return this.http.get<any>(`${environment.apiNutritionUrl}/Nutritional/GetAllPatients`).pipe(
        catchError((_: HttpErrorResponse, __: Observable<any>) => {
            return of([]);
        })
    );
}

public getLastPrescription(): Observable<any> {
  return this.http.get<any>(`${environment.apiNutritionUrl}/Nutritional/GetLastPrescription`).pipe(
      catchError((_:HttpErrorResponse, __: Observable<any>) => {
          return of([]);
      })
  );
}

}
