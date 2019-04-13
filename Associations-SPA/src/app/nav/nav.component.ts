import { Component, OnInit } from '@angular/core';
import { WordsService } from '../_services/words.service';
import { Words } from '../_interfaces/Words';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  selected: string;
  words: string[] = [];

  constructor(private wordsService: WordsService) { }

  ngOnInit() {
    this.loadWords();
  }

  loadWords() {
    this.wordsService.getAll().subscribe((data: Words[]) => {
      if (data) {
       this.words = data.map(d => d.word);
      }
    });
  }

}
