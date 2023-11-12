import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from "./layouts/layout/layout.component";
import {Lab1Component} from "./labs/lab1/lab1.component";

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            {path: '', loadChildren: () => import('./labs/labs.module').then(m => m.LabsModule)},
        ],
        title: 'Sopad'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
