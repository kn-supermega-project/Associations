import { NgModule } from '@angular/core';
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {TooltipModule} from 'ngx-tooltip';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { AppComponent } from './app.component';
import { TotalGraphComponent } from './total-graph/total-graph.component';
import { WordsService } from './_services/words.service';


@NgModule({
   declarations: [
      AppComponent,
      TotalGraphComponent,
      TotalGraphComponent
   ],
   imports: [
      BrowserModule,
      NgxGraphModule,
      NgxChartsModule,
      TooltipModule,
      HttpClientModule,
      BrowserAnimationsModule,
      BrowserModule,
      TabsModule.forRoot()
   ],
   providers: [
      WordsService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
