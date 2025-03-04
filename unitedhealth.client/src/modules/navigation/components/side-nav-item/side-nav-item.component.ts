import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { AuthService } from '@modules/auth/services/auth.service';
import { SBRouteData, SideNavItem } from '@modules/navigation/models';

@Component({
    selector: 'sb-side-nav-item',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './side-nav-item.component.html',
    styleUrls: ['side-nav-item.component.scss'],
})
export class SideNavItemComponent implements OnInit {
    @Input() sideNavItem!: SideNavItem;
    @Input() isActive!: boolean;

    expanded = false;
    routeData!: SBRouteData;

    constructor(public authService: AuthService) {}
    ngOnInit() {}

    isItemVisible(profiles: string[]): boolean{
        if(profiles){
            return profiles.includes(this.authService.userValue?.profile!);
        }
        return true;
    }
}
