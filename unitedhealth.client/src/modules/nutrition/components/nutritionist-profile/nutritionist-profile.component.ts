import { Component, OnInit } from '@angular/core';
import { NutritionService } from '@modules/nutrition/services';
import { AuthService } from '@modules/auth/services/auth.service';
import { AlertService } from '@common/services';

@Component({
  selector: 'sb-nutritionist-profile',
  templateUrl: './nutritionist-profile.component.html',
  styleUrls: ['./nutritionist-profile.component.scss']
})
export class NutritionistProfileComponent implements OnInit {
  freeTimeSlot: string = '';
  nutritionistName: string = '';
  nutritionistEmail: string = '';
  nutritionistPhone: string = '';
  nutritionistUsername: string = '';
  nutritionistBirthDate: Date = new Date() ;

  constructor(private service: NutritionService, public authService: AuthService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.nutritionistPhone = this.authService.userValue?.phoneNumber ?? "Undefined";
    this.nutritionistName = this.authService.userValue?.name ?? "Undefined" ;
    this.nutritionistEmail = this.authService.userValue?.email ?? "Undefined" ;
    this.nutritionistBirthDate = this.authService.userValue?.birthDate ?? new Date() ;
    this.nutritionistUsername = this.authService.userValue?.username ?? "Undefined";
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
