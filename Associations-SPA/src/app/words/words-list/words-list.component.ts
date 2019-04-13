import { Component, OnInit } from '@angular/core';
import { WordsService } from 'src/app/_services/words.service';
import { WordsToList } from 'src/app/_interfaces/WordsToList';

@Component({
  selector: 'app-words-list',
  templateUrl: './words-list.component.html',
  styleUrls: ['./words-list.component.css']
})
export class WordsListComponent implements OnInit {

  wordsToList: WordsToList[];

  constructor(private wordsService: WordsService) { }

  ngOnInit() {
    this.loadWords();
  }

  loadWords() {
    this.wordsService.getMainWords().subscribe((data: WordsToList[]) => {
      if (data) {
        this.wordsToList = data;
      }
    });
  }
}
