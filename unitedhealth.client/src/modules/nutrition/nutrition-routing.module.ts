import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from '@modules/navigation/models';

/* Containers */
import * as dashboardComponents from './components';

import { NutritionModule } from './nutrition.module';
import { NutritionistGuard } from '@modules/auth/guards/nutritionist.guard';
import { PatientGuard } from '@modules/auth/guards/patient.guard';


const routes: Routes = [
  {
    path: '',
    data: {
        title: 'Nutrition: Patient area', //name of the tab
        breadcrumbs: [
            {
                text: 'Nutrition',
                active: true,
            },
        ],
    } as SBRouteData,
    canActivate: [PatientGuard],
    component: dashboardComponents.PatientComponent,
  },
  {
    path: 'nutritionist-profile',
    data: {
        title: 'Nutrition : Personal Profile',
        breadcrumbs: [
            {
                text: 'Nutrition',
                active: true,
            }
        ],
    } as SBRouteData,
    canActivate: [NutritionistGuard],
    component: dashboardComponents.NutritionistProfileComponent,
  },
  {
    path: 'appointments',
    data: {
        title: 'Nutrition : Consultas',
        breadcrumbs: [
            {
                text: 'Nutrition',
                active: true,
            }
        ],
    } as SBRouteData,
    canActivate: [NutritionistGuard],
    component: dashboardComponents.AppointmentsComponent,
  },
  {
    path: 'patients',
    data: {
        title: 'Nutrition : Pacientes',
        breadcrumbs: [
            {
                text: 'Nutrition',
                active: true,
            }
        ],
    } as SBRouteData,
    canActivate: [NutritionistGuard],
    component: dashboardComponents.PatientListComponent,
  },
];

@NgModule({
  imports: [NutritionModule, RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NutritionRoutingModule { }
