import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 

import { NavigationModule } from '@modules/navigation/navigation.module';

/* Components */
import * as nutritionComponents from './components';

/* Services */
import * as nutritionServices from './services';


@NgModule({
  declarations: [...nutritionComponents.components],
  providers: [...nutritionServices.services],
  exports: [...nutritionComponents.components],
  imports: [
    CommonModule,
    NavigationModule,
    FormsModule
  ]
})
export class NutritionModule { }
