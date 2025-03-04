import { Component, OnInit } from '@angular/core';
import { MedicalService } from '@modules/medical/services';
import { AlertService } from '@common/services'; // Ensure you import AlertService
import { HttpErrorResponse } from '@angular/common/http';  // Import this to handle HTTP errors


@Component({
  selector: 'sb-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.scss']
})
export class DoctorComponent implements OnInit {
  selectedSpecialty: string = '';
  freeTimeSlot: string = '';
  consultations: any[] = [];
  specialties: string[] = [];
  public prescription: string = '';
  public selectedConsultation: any = null;

  constructor(
    private medicalService: MedicalService,
    private alertService: AlertService // Add alertService in the constructor
  ) { }

  ngOnInit(): void {
    this.medicalService.getCurrentSpecialty().subscribe({
      next: (data) => {
        this.selectedSpecialty = data.name;
      },
      error: (error: HttpErrorResponse) => {
        this.alertService.error('Failed to fetch current specialty. Please check your connection or try again later.');
      }
    });
  
    this.medicalService.getSpecialties().subscribe({
      next: (data) => {
        this.specialties = data;
      },
      error: (error: HttpErrorResponse) => {
        this.alertService.error('Failed to fetch specialties. Please check your connection or try again later.');
      }
    });
  }

  onSelectSpecialty(specialty: string): void {
    this.selectedSpecialty = specialty;
  }


  submitSpecialty() {
    this.medicalService.insertSpecialty(this.selectedSpecialty).subscribe(() => {
      this.alertService.success('Specialty submitted successfully');
    }, (error) => {
      this.alertService.error('Error submitting specialty:', error);
    });
  }

  submitFreeTimeSlot() {
    const formattedDate = new Date(this.freeTimeSlot).toLocaleString('sv-SE').replace(',', '');
    this.medicalService.insertFreeTimeSlot(formattedDate).subscribe(() => {
      this.alertService.success('Time slot saved successfully');
    }, (error) => {
      this.alertService.error('Error submitting free time slot:', error);
    });
  }

  loadConsultations(): void {
    this.medicalService.getAllConsultations().subscribe(data => {
        this.consultations = data;
        if (this.consultations.length === 0) {
            this.alertService.info('No consultations scheduled yet.');
        }
    }, error => {
        this.alertService.error('Failed to load consultations');
    });
}

  submitPrescription() {
    if (this.selectedConsultation) {
      this.medicalService.insertMedicalPrescription(this.selectedConsultation.appointmentId, this.selectedConsultation.prescription).subscribe(() => {
        this.alertService.success('Prescription updated successfully');
        this.selectedConsultation.isCompleted = true;
        this.selectedConsultation = null; 
      }, (error) => {
        this.alertService.error('Error updating prescription:', error);
      });
    }
  }

  openPrescriptionModal(consultation: any) {
    this.selectedConsultation = consultation;
  }
  
}
