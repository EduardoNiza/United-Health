import { Component, OnInit } from '@angular/core';
import { AlertService } from '@common/services';
import { NutritionService } from '@modules/nutrition/services';

@Component({
  selector: 'sb-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.scss']
})
export class PatientComponent implements OnInit {

  nutritionists: any[] = [];
  selectedNutritionist: any = null;
  selectedTimeSlot: string | null = null;
  consultations: any[] = [];
  passedconsultations: any[] = [];
  futureconsultations: any[] = [];

  constructor(private nutritionService: NutritionService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.loadConsultations();
    this.nutritionService.getAllNutritionists().subscribe(data => {
      this.nutritionists = data;
    });
  }

  selectTimeSlot(timeSlot: string): void {
    this.selectedTimeSlot = timeSlot;
  }

  onSelectNutritionist(nutritionist: any): void {
    this.selectedNutritionist = nutritionist;
  }

  getNutritionist(): any[] {
    return this.nutritionists;
  }

  onScheduleConsultation(): void {
    if (this.selectedTimeSlot && this.selectedNutritionist) {
      this.nutritionService.insertConsultation(this.selectedTimeSlot, this.selectedNutritionist.id).subscribe({
        next: (response) => {
          this.alertService.success('Consulta marcada com sucesso');
          this.selectedNutritionist.freeTimeSlots = this.selectedNutritionist.freeTimeSlots
            .split(', ')
            .filter((slot: string) => slot !== this.selectedTimeSlot)
            .join(', ');
          this.selectedTimeSlot = null;
        },
        error: (error) => {
          this.alertService.error('Erro ao marcar consulta', error);
        }
      });
    }
  }

  loadConsultations(): void {
    this.nutritionService.getAllConsultations().subscribe(data => {
        this.consultations = data;
        this.separateConsultationsByDate();
    });
}

updateConsultationStatus(consultation: any) {
  this.nutritionService.updateConsultationStatus(consultation.appointmentId).subscribe(() => {
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