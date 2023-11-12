import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Lab1Component} from "./lab1/lab1.component";
import {Lab2Component} from "./lab2/lab2.component";
import {Lab4Component} from "./lab4/lab4.component";
import {Lab3Component} from "./lab3/lab3.component";
import {Lab5Component} from "./lab5/lab5.component";

const routes: Routes = [
    {path: '', pathMatch: 'full', redirectTo: 'lab1'},
    {path: 'lab1', component: Lab1Component, title: 'lab1'},
    {path: 'lab2', component: Lab2Component, title: 'lab2'},
    {path: 'lab3', component: Lab3Component, title: 'lab3'},
    {path: 'lab4', component: Lab4Component, title: 'lab4'},
    {path: 'lab5', component: Lab5Component, title: 'lab5'},
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LabsRoutingModule {
}
