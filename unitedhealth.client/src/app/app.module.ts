import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from '@modules/auth/guards/auth.interceptor';
import { AuthGuard } from '@modules/auth/guards/auth.guard';
import { AuthService } from '@modules/auth/services/auth.service';
import { TrainerGuard } from '@modules/auth/guards/trainer.guard';
import { PatientGuard } from '@modules/auth/guards/patient.guard';
import { NutritionistGuard } from '@modules/auth/guards/nutritionist.guard';
import { MedicalGuard } from '@modules/auth/guards/medical.guard';

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, AppRoutingModule, HttpClientModule],
    providers: [{
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true
      },
        AuthGuard,
        TrainerGuard,
        PatientGuard,
        NutritionistGuard,
        MedicalGuard,
        AuthService],
    bootstrap: [AppComponent],
})
export class AppModule {}
