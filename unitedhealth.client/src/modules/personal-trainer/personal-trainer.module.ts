import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 

import { NavigationModule } from '@modules/navigation/navigation.module';

/* Components */
import * as personalTrainerComponents from './components';

/* Services */
import * as personalTrainerServices from './services';


@NgModule({
  declarations: [...personalTrainerComponents.components],
  providers: [...personalTrainerServices.services],
  exports: [...personalTrainerComponents.components],
  imports: [
    CommonModule,
    NavigationModule,
    FormsModule
  ]
})
export class PersonalTrainerModule { }
