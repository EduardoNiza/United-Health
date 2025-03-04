import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class PersonalTrainerService {
    constructor(private http: HttpClient) {}

    public getAllTrainer() {
      return this.http.get<any>(`${environment.apiTrainingUrl}/Training/GetAllTrainer`).pipe(
        catchError((_: HttpErrorResponse, __: Observable<any>) => {
          return of({});
        }));
    }

    public getPersonalDetails() {
      return this.http.get<any>(`${environment.apiTrainingUrl}/Training/GetPersonalDetails`).pipe(
        catchError((_: HttpErrorResponse, __: Observable<any>) => {
          return of({});
        }));
    }

    public insertConsultation(date: string, trainerId: string): Observable<{ message: string }> {
      const params = new HttpParams().set('date', date).set('trainerId', trainerId);
      const url = `${environment.apiTrainingUrl}/Training/InsertConsultation`;
      return this.http.post<{ message: string }>(url, null, { params })
        .pipe(
          catchError((error) => {
            return throwError(() => new Error('Error inserting consultation'));
          })
        );
    }

    insertFreeTimeSlot(date: string): Observable<{ message: string }> {
      const params = new HttpParams().set('date', date);
      const url = `${environment.apiTrainingUrl}/Training/InsertFreeTimeSlot`;
      return this.http.post<{ message: string }>(url, null, { params })
        .pipe(
          catchError((error) => {
            return throwError(() => new Error('Error inserting free time slot'));
          })
        );
    }

    public getAllConsultations(): Observable<any> {
      return this.http.get<any>(`${environment.apiTrainingUrl}/Training/GetAllConsultation`).pipe(
          catchError((_: HttpErrorResponse, __: Observable<any>) => {
              return of([]);
          })
      );
  }



  public getAllPatients(): Observable<any> {
    return this.http.get<any>(`${environment.apiTrainingUrl}/Training/GetAllPatients`).pipe(
        catchError((_: HttpErrorResponse, __: Observable<any>) => {
            return of([]);
        })
    );
}

  public updateConsultationStatus(appointmentId: number): Observable<{ message: string }> {
    const params = new HttpParams().set('appointmentId', appointmentId.toString());
    const url = `${environment.apiTrainingUrl}/Training/CancelConsultation`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error updating consultation status'));
        })
      );
  }

  public insertTrainingPrescription(appointmentId: number, prescription: string): Observable<{ message: string }> {
    const params = new HttpParams().set('appointmentId', appointmentId.toString()).set('workouts', prescription);
    const url = `${environment.apiTrainingUrl}/Training/InsertWorkouts`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error inserting workout'));
        })
      );
  }
}
