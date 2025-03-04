import { Component, OnInit } from '@angular/core';
import { AlertService } from '@common/services';
import { PersonalTrainerService } from '@modules/personal-trainer/services';

@Component({
  selector: 'sb-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.scss']
})
export class PatientComponent implements OnInit {

  doctors: any[] = [];
  selectedSpecialty: string | null = null;
  selectedDoctor: any = null;
  selectedTimeSlot: string | null = null;
  consultations: any[] = [];
  passedconsultations: any[] = [];
  futureconsultations: any[] = [];

  constructor(private personaltrainerService: PersonalTrainerService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.loadConsultations();
    this.personaltrainerService.getAllTrainer().subscribe(data => {
      this.doctors = data;
    });
  }

  selectTimeSlot(timeSlot: string): void {
    this.selectedTimeSlot = timeSlot;
  }

  onSelectSpecialty(specialty: string): void {
    this.selectedSpecialty = specialty;
  }

  onSelectDoctor(doctor: any): void {
    this.selectedDoctor = doctor;
  }

  getTrainer(): any[] {
    return this.doctors;
  }

  onScheduleConsultation(): void {
    if (this.selectedTimeSlot && this.selectedDoctor) {
      this.personaltrainerService.insertConsultation(this.selectedTimeSlot, this.selectedDoctor.id).subscribe({
        next: (response) => {
          this.alertService.success('Appointment made successfully');
          this.selectedDoctor.freeTimeSlots = this.selectedDoctor.freeTimeSlots
            .split(', ')
            .filter((slot: string) => slot !== this.selectedTimeSlot)
            .join(', ');
          this.selectedTimeSlot = null;
        },
        error: (error) => {
          this.alertService.error('Error when setting appointment', error);
        }
      });
    }
  }

  loadConsultations(): void {
    this.personaltrainerService.getAllConsultations().subscribe(data => {
        this.consultations = data;
        this.separateConsultationsByDate();
    });
}

updateConsultationStatus(consultation: any) {
  this.personaltrainerService.updateConsultationStatus(consultation.appointmentId).subscribe(() => {
    this.alertService.success('Consultation status updated successfully');
    consultation.isCompleted = true; 
  }, (error) => {
    this.alertService.error('Error updating consultation status:', error);
  });
}

separateConsultationsByDate(): void {
  const currentDate = new Date();
  this.passedconsultations = this.consultations.filter(consultation => new Date(consultation.scheduled) < currentDate);
  this.futureconsultations = this.consultations.filter(consultation => new Date(consultation.scheduled) >= currentDate);
}

}
