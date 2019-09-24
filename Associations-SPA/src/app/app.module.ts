import { NgModule } from '@angular/core';
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {TooltipModule} from 'ngx-tooltip';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { GraphComponent } from './graph/graph.component';
import { WordsService } from './_services/words.service';
import { NavComponent } from './nav/nav.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { WordsListComponent } from './words/words-list/words-list.component';
import { WordsDetailsComponent } from './words/words-details/words-details.component';
import { ApiService } from './_services/api.service';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { WordAddComponent } from './words/WordAdd/WordAdd.component';


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      GraphComponent,
      WordsListComponent,
      WordsDetailsComponent,
      WordAddComponent
   ],
   imports: [
      BrowserModule,
      NgxGraphModule,
      NgxChartsModule,
      TooltipModule,
      HttpClientModule,
      BrowserAnimationsModule,
      BrowserModule,
      TabsModule.forRoot(),
      TypeaheadModule.forRoot(),
      ModalModule.forRoot(),
      AngularFontAwesomeModule,
      FormsModule,
      RouterModule.forRoot(appRoutes),
      ReactiveFormsModule
   ],
   providers: [
      ApiService,
      WordsService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
