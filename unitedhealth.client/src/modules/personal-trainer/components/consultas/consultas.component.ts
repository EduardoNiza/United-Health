import { Component, OnInit } from '@angular/core';
import { AlertService } from '@common/services';
import { PersonalTrainerService } from '@modules/personal-trainer/services';

@Component({
  selector: 'sb-consultas',
  templateUrl: './consultas.component.html',
  styleUrls: ['./consultas.component.scss']
})
export class ConsultasComponent implements OnInit {
  freeTimeSlot: string = '';
  consultations: any[] = [];
  passedconsultations: any[] = [];
  futureconsultations: any[] = [];
  public prescription: string = '';
  public selectedConsultation: any = null;
  
  constructor(private service: PersonalTrainerService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.loadConsultations();
  }

  loadConsultations(): void {
    this.service.getAllConsultations().subscribe(data => {
        this.consultations = data;
        this.separateConsultationsByDate();
    });
}

  separateConsultationsByDate(): void {
    const currentDate = new Date();
    this.passedconsultations = this.consultations.filter(consultation => new Date(consultation.scheduled) < currentDate);
    this.futureconsultations = this.consultations.filter(consultation => new Date(consultation.scheduled) >= currentDate);
  }

  openPrescriptionModal(consultation: any) {
    this.selectedConsultation = consultation;
  }

  submitPrescription(){
    if (this.selectedConsultation) {
      this.service.insertTrainingPrescription(this.selectedConsultation.appointmentId, this.selectedConsultation.prescription).subscribe(() => {
        this.alertService.success('Prescription updated successfully');
        this.selectedConsultation.isCompleted = true; 
        this.selectedConsultation = null; 
      }, (error) => {
        this.alertService.error('Error updating prescription:', error);
      });
    }
  }

}
