import { Component, OnInit } from '@angular/core';
import { AuthService } from '@modules/auth/services/auth.service';
import { MedicalService } from '@modules/medical/services';

@Component({
  selector: 'sb-medical',
  templateUrl: './medical.component.html',
  styleUrls: ['./medical.component.scss']
})
export class MedicalComponent implements OnInit {
  

  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

  isDoctor(){
    return this.authService.userValue?.profile === "Medical";
  }
}
