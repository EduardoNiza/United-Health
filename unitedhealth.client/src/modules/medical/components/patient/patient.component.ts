import { Component, OnInit } from '@angular/core';
import { AlertService } from '@common/services';
import { MedicalService } from '@modules/medical/services';

@Component({
  selector: 'sb-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.scss']
})
export class PatientComponent implements OnInit {

  doctors: any[] = [];
  selectedSpecialty: string | null = null;
  specialties: string[] = [];
  selectedDoctor: any = null;
  selectedTimeSlot: string | null = null;
  consultations: any[] = [];

  constructor(private medicalService: MedicalService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.medicalService.getSpecialties().subscribe(data => {
      this.specialties = data;
    })
  }

  selectTimeSlot(timeSlot: string): void {
    this.selectedTimeSlot = timeSlot;
  }

  onSelectSpecialty(specialty: string): void {
    this.selectedSpecialty = specialty;
    this.selectedDoctor = null;
  
    this.loadDoctors(specialty);
  }
  

  onSelectDoctor(doctor: any): void {
    this.selectedDoctor = doctor;
  }

  onScheduleConsultation(): void {
    if (this.selectedTimeSlot && this.selectedDoctor) {
      this.medicalService.insertConsultation(this.selectedTimeSlot, this.selectedDoctor.id).subscribe({
        next: (response) => {
          this.alertService.success('Appointment scheduled successfully.');
          this.selectedDoctor.freeTimeSlots = this.selectedDoctor.freeTimeSlots
            .split(', ')
            .filter((slot: string) => slot !== this.selectedTimeSlot)
            .join(', ');
          this.selectedTimeSlot = null;
        },
        error: (error) => {
          this.alertService.error('Error when making an appointment:', error);
        }
      });
    } else {
      this.alertService.error('No time slot or doctor selected.');
    }
  }
  

  loadConsultations(): void {
    this.medicalService.getAllConsultations().subscribe({
      next: (data) => {
        this.consultations = data;
        if (data.length > 0) {
          this.alertService.success('Consultations loaded successfully.');
        } else {
          this.alertService.info('No consultations found.');
        }
      },
      error: (error) => {
        this.alertService.error('Error loading consultations:', error);
      }
    });
  }
  

  loadDoctors(specialty: string) {
    this.medicalService.getDoctorsBySpecialty(specialty).subscribe({
      next: (data) => {
        this.doctors = data;
        this.alertService.success('Doctors loaded successfully.');
      },
      error: (error) => {
        this.alertService.error('Error loading doctors:', error);
      }
    });
  }

  updateConsultationStatus(consultation: any) {
    this.medicalService.cancelConsultation(consultation.appointmentId).subscribe({
      next: () => {
        this.alertService.success('Consultation status updated successfully.');
        consultation.isCompleted = true;
      },
      error: (error) => {
        this.alertService.error('Error updating consultation status:', error);
      }
    });
  }
  


}
