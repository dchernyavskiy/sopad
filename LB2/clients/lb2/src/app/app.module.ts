import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {ServerApiModule} from "./api/server/server-api.module";
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ServerApiModule.forRoot({
      rootUrl: "http://localhost:5000"
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
