import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WordsRoutingModule } from './words-routing.module';
import { WordsListComponent } from './words-list/words-list.component';

@NgModule({
  imports: [
    CommonModule,
    WordsRoutingModule
  ],
  declarations: [WordsListComponent]
})
export class WordsModule { }
