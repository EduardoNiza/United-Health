import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from '@modules/navigation/models';

/* Containers */
import * as dashboardComponents from './components';

import { MedicalModule } from './medical.module';
import { MedicalGuard } from '@modules/auth/guards/medical.guard';

const routes: Routes = [
  {
    path: '',
    data: {
        title: 'Medical',
        breadcrumbs: [
            {
                text: 'Medical',
                active: true,
            },
        ],
    } as SBRouteData,
    canActivate: [MedicalGuard],
    component: dashboardComponents.MedicalComponent,
  }
];

@NgModule({
  imports: [MedicalModule, RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicalRoutingModule { }
