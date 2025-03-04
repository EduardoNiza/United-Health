import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@modules/auth/guards/auth.guard';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: '/dashboard'
    },
    {
        path: 'dashboard',
        loadChildren: () =>
            import('modules/dashboard/dashboard-routing.module').then(
                m => m.DashboardRoutingModule
            ),
        canActivate: [AuthGuard]
    },
    {
        path: 'medical',
        loadChildren: () =>
            import('modules/medical/medical-routing.module').then(
                m => m.MedicalRoutingModule
            ),
        canActivate: [AuthGuard]
    },
    {
        path: 'nutrition',
        loadChildren: () =>
            import('modules/nutrition/nutrition-routing.module').then(
                m => m.NutritionRoutingModule
            ),
        canActivate: [AuthGuard]
    },
    {
        path: 'personal-trainer',
        loadChildren: () =>
            import('modules/personal-trainer/personal-trainer-routing.module').then(
                m => m.PersonalTrainerRoutingModule
            ),
        canActivate: [AuthGuard]
    },
    {
        path: 'auth',
        loadChildren: () =>
            import('modules/auth/auth-routing.module').then(m => m.AuthRoutingModule),
    },
    {
        path: 'error',
        loadChildren: () =>
            import('modules/error/error-routing.module').then(m => m.ErrorRoutingModule),
    },
    {
        path: '**',
        pathMatch: 'full',
        loadChildren: () =>
            import('modules/error/error-routing.module').then(m => m.ErrorRoutingModule),
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule],
})
export class AppRoutingModule {}
