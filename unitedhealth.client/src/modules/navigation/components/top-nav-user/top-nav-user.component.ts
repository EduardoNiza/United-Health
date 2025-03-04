import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@modules/auth/services/auth.service';

@Component({
    selector: 'sb-top-nav-user',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './top-nav-user.component.html',
    styleUrls: ['top-nav-user.component.scss'],
})
export class TopNavUserComponent implements OnInit {
    constructor(public authService: AuthService, public router: Router, public cdr: ChangeDetectorRef) {}

    user: string | undefined;

    ngOnInit() {
        this.user = this.authService.userValue?.name         
    }

    public logout(){
        this.authService.signOut();  
    }
}
