import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { DashboardService } from '@modules/dashboard/services';

@Component({
    selector: 'sb-dashboard-cards',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './dashboard-cards.component.html',
    styleUrls: ['dashboard-cards.component.scss'],
})
export class DashboardCardsComponent implements OnInit {
    diet: any;
    workout: any;

    constructor(private dashboardService: DashboardService, private cdr: ChangeDetectorRef) {}
    ngOnInit() {
        this.dashboardService.getLastWorkout().subscribe(w => {
            this.workout = w;
            this.cdr.detectChanges();
        })
        this.dashboardService.getLastDiet().subscribe(d => {
            this.diet = d;
            this.cdr.detectChanges()
        })
    }

}
