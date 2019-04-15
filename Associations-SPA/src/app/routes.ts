import { Routes, RouterModule } from '@angular/router';
import { GraphComponent } from './graph/graph.component';
import { WordsListComponent } from './words/words-list/words-list.component';
import { WordsDetailsComponent } from './words/words-details/words-details.component';

export const appRoutes: Routes = [
  {path: '', component: GraphComponent},
  {
    path: '',
    children: [
      {path: 'words', component: WordsListComponent},
      {path: 'words/:id', component: WordsDetailsComponent}
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'  }
];

