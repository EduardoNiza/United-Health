import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from '@modules/navigation/models';

/* Containers */
import * as dashboardComponents from './components';

import { PersonalTrainerModule } from './personal-trainer.module';
import { TrainerGuard } from '@modules/auth/guards/trainer.guard';
import { PatientGuard } from '@modules/auth/guards/patient.guard';

const routes: Routes = [
  {
    path: '',
    data: {
        title: 'Personal Trainer: Patient area', //name of the tab
        breadcrumbs: [
            {
                text: 'Personal Trainer',
                active: true,
            },
        ],
    } as SBRouteData,
    canActivate: [PatientGuard],
    component: dashboardComponents.PatientComponent,
  },
  {
    path: 'personal-profile',
    data: {
        title: 'Personal Trainer : Personal Profile',
        breadcrumbs: [
            {
                text: 'Personal Trainer',
                active: true,
            }
        ],
    } as SBRouteData,
    canActivate: [TrainerGuard],
    component: dashboardComponents.PersonalProfileComponent,
  },
  {
    path: 'consultas',
    data: {
        title: 'Personal Trainer : Consultas',
        breadcrumbs: [
            {
                text: 'Personal Trainer',
                active: true,
            }
        ],
    } as SBRouteData,
    canActivate: [TrainerGuard],
    component: dashboardComponents.ConsultasComponent,
  },
  {
    path: 'pacientes',
    data: {
        title: 'Personal Trainer : Pacientes',
        breadcrumbs: [
            {
                text: 'Personal Trainer',
                active: true,
            }
        ],
    } as SBRouteData,
    canActivate: [TrainerGuard],
    component: dashboardComponents.PatientListComponent,
  },
];

@NgModule({
  imports: [PersonalTrainerModule, RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PersonalTrainerRoutingModule { }
