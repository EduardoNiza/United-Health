import { Component, OnInit } from '@angular/core';
import { PersonalTrainerService } from '@modules/personal-trainer/services';

@Component({
  selector: 'sb-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.scss']
})
export class PatientListComponent implements OnInit {
  patients: any[] = [];

  constructor(private service: PersonalTrainerService) { }

  ngOnInit(): void {
    this.getAllPatients();
  }

  getAllPatients(): void {
    this.service.getAllPatients().subscribe(data => {
        this.patients = data;
    });
}

}
