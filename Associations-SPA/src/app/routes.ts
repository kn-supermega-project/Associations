import { Routes, RouterModule } from '@angular/router';
import { GraphComponent } from './graph/graph.component';
import { WordsListComponent } from './words/words-list/words-list.component';
import { WordsDetailsComponent } from './words/words-details/words-details.component';
import { WordAddComponent } from './words/WordAdd/WordAdd.component';

export const appRoutes: Routes = [
  {path: '', component: GraphComponent},
  {
    path: '',
    children: [
      {path: 'words', component: WordsListComponent},
      {path: 'words/:id', component: WordsDetailsComponent},
      {path: 'wordadd', component: WordAddComponent}
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'  }
];

