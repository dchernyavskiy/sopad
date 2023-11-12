import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LabsRoutingModule } from './labs-routing.module';
import { Lab1Component } from './lab1/lab1.component';
import {FormsModule} from "@angular/forms";
import { Lab2Component } from './lab2/lab2.component';
import { Lab3Component } from './lab3/lab3.component';
import { Lab4Component } from './lab4/lab4.component';
import { Lab5Component } from './lab5/lab5.component';


@NgModule({
  declarations: [
    Lab1Component,
    Lab2Component,
    Lab3Component,
    Lab4Component,
    Lab5Component
  ],
    imports: [
        CommonModule,
        LabsRoutingModule,
        FormsModule
    ]
})
export class LabsModule { }
