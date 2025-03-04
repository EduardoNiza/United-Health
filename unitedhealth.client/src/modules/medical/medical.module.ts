import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { NavigationModule } from '@modules/navigation/navigation.module';

/* Components */
import * as medicalComponents from './components';

/* Services */
import * as medicalServices from './services';
import { PatientComponent } from './components/patient/patient.component';
import { DoctorComponent } from './components/doctor/doctor.component';


@NgModule({
  declarations: [...medicalComponents.components, PatientComponent, DoctorComponent],
  providers: [...medicalServices.services],
  exports: [...medicalComponents.components],
  imports: [
    CommonModule,
    FormsModule,
    NavigationModule
  ]
})
export class MedicalModule { }
