import { Component, OnInit } from '@angular/core';
import { AuthService } from '@modules/auth/services/auth.service';

@Component({
    selector: 'sb-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
    constructor(private authService: AuthService) {}
    ngOnInit() {}

    isProfile(role: string){
        return this.authService.userValue?.profile === role;
    }
}
