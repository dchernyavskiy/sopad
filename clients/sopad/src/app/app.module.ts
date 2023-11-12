import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {LayoutComponent} from './layouts/layout/layout.component';
import {Lab1ApiModule} from "./api/lab1/lab-1-api.module";
import {Lab2ApiModule} from "./api/lab2/lab-2-api.module";

@NgModule({
    declarations: [
        AppComponent,
        LayoutComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        Lab1ApiModule.forRoot({
            rootUrl: "http://localhost:5000"
        }),
        Lab2ApiModule.forRoot({
            rootUrl: "http://localhost:5002"
        }),
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
