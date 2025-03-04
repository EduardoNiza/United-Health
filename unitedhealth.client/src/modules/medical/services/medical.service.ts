import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MedicalService {
  constructor(private http: HttpClient) { }

  public getDoctorsBySpecialty(specialty: string) {
    const params = new HttpParams().set('specialty', specialty);
    return this.http.get<any>(`${environment.apiMedicalUrl}/Medical/GetDoctors`, {params}).pipe(
      catchError((_: HttpErrorResponse, __: Observable<any>) => {
        return of({});
      }));
  }

  public getSpecialties() {
    return this.http.get<any>(`${environment.apiMedicalUrl}/Medical/GetSpecialties`).pipe(
      catchError((_: HttpErrorResponse, __: Observable<any>) => {
        return of({});
      }));
  }

  public getCurrentSpecialty() {
    return this.http.get<any>(`${environment.apiMedicalUrl}/Medical/GetCurrentSpecialty`).pipe(
      catchError((_: HttpErrorResponse, __: Observable<any>) => {
        return of({});
      }));
  }

  public insertSpecialty(specialty: string) {
    const urlWithQueryParam = `${environment.apiMedicalUrl}/Medical/InsertSpecialty?specialty=${encodeURIComponent(specialty)}`;

    return this.http.post<any>(urlWithQueryParam, {}).pipe(
      catchError((error: HttpErrorResponse) => {
        return of({});
      })
    );
  }

  insertFreeTimeSlot(date: string): Observable<{ message: string }> {
    const params = new HttpParams().set('date', date);
    const url = `${environment.apiMedicalUrl}/Medical/InsertFreeTimeSlot`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error inserting free time slot'));
        })
      );
  }

  public insertConsultation(date: string, doctorId: string): Observable<{ message: string }> {
    const params = new HttpParams().set('date', date).set('doctorId', doctorId);
    const url = `${environment.apiMedicalUrl}/Medical/InsertConsultation`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error inserting consultation'));
        })
      );
  }

  public getAllConsultations(): Observable<any> {
    return this.http.get<any>(`${environment.apiMedicalUrl}/Medical/GetAllConsultation`).pipe(
      catchError((_: HttpErrorResponse, __: Observable<any>) => {
        return of([]);
      })
    );
  }

  public insertMedicalPrescription(appointmentId: number, medicalPrescription: string): Observable<{ message: string }> {
    const params = new HttpParams().set('appointmentId', appointmentId.toString()).set('medicalPrescription', medicalPrescription);
    const url = `${environment.apiMedicalUrl}/Medical/InsertMedicalPrescription`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error inserting medical prescription'));
        })
      );
  }

  public cancelConsultation(appointmentId: number): Observable<{ message: string }> {
    const params = new HttpParams().set('appointmentId', appointmentId.toString());
    const url = `${environment.apiMedicalUrl}/Medical/CancelConsultation`;
    return this.http.post<{ message: string }>(url, null, { params })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error('Error updating consultation status'));
        })
      );
  }



}
