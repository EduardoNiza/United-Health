import { Component, OnInit } from '@angular/core';
import { PersonalTrainerService } from '@modules/personal-trainer/services';
import { AuthService } from '@modules/auth/services/auth.service';
import { AlertService } from '@common/services';

@Component({
  selector: 'sb-personal-profile',
  templateUrl: './personal-profile.component.html',
  styleUrls: ['./personal-profile.component.scss']
})
export class PersonalProfileComponent implements OnInit {
  freeTimeSlot: string = '';
  personalTrainerName: string = '';
  personalTrainerType: string = '';
  personalTrainerEmail: string = '';
  personalTrainerPhone: string = '';
  personalTrainerUsername: string = '';
  personalTrainerBirthDate: Date = new Date() ;

  constructor(private service: PersonalTrainerService, public authService: AuthService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.personalTrainerType = this.authService.userValue?.profile ?? "Undefined";
    this.personalTrainerPhone = this.authService.userValue?.phoneNumber ?? "Undefined";
    this.personalTrainerName = this.authService.userValue?.name ?? "Undefined" ;
    this.personalTrainerEmail = this.authService.userValue?.email ?? "Undefined" ;
    this.personalTrainerBirthDate = this.authService.userValue?.birthDate ?? new Date() ;
    this.personalTrainerUsername = this.authService.userValue?.username ?? "Undefined";
  }

  submitFreeTimeSlot() {  
    const formattedDate = new Date(this.freeTimeSlot).toLocaleString('sv-SE').replace(',', '');
    
    this.service.insertFreeTimeSlot(formattedDate).subscribe(() => {
      this.alertService.success('Free time slot submitted successfully');
    }, (error) => {
      this.alertService.error('Error submitting free time slot:', error);
    });
  }


}
