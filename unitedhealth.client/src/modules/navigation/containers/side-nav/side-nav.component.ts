import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '@modules/auth/services/auth.service';
import { SideNavItems, SideNavSection } from '@modules/navigation/models';
import { NavigationService } from '@modules/navigation/services';

@Component({
    selector: 'sb-side-nav',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './side-nav.component.html',
    styleUrls: ['side-nav.component.scss'],
})
export class SideNavComponent implements OnInit {
    @Input() sidenavStyle!: string;
    @Input() sideNavItems!: SideNavItems;
    @Input() sideNavSections!: SideNavSection[];

    constructor(public navigationService: NavigationService, public authService: AuthService, public cdr: ChangeDetectorRef) {}

    user: string | undefined;

    ngOnInit() {
        this.user = this.authService.userValue?.name    
    }

    isItemVisible(profiles: string[]): boolean{
        if(profiles){
            return profiles.includes(this.authService.userValue?.profile!);
        }
        return true;
    }
}
