import { Component, OnInit } from '@angular/core';
import { WordsService } from 'src/app/_services/words.service';
import { Words } from 'src/app/_interfaces/Words';
import { FormGroup, FormControl } from '@angular/forms';
import { MainWordRequest } from 'src/app/RequestModels/MainWord-request';
import { RelWordRequest } from 'src/app/RequestModels/RelWord-requst';
import { WordRelsService } from 'src/app/_services/WordRels.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-WordAdd',
  templateUrl: './WordAdd.component.html',
  styleUrls: ['./WordAdd.component.css']
})
export class WordAddComponent implements OnInit {

  constructor(private wordsService: WordsService,
    private relService: WordRelsService,
    private alertify: AlertifyService) {
    this.CreateMainForm = new FormGroup({
      addMain: new FormControl(),
      Type: new FormControl()
   });
   this.CreateRelation = new FormGroup({
     MPoints: new FormControl(),
     FPoints: new FormControl(),
     TPoints: new FormControl(),
     MPer: new FormControl(),
     FPer: new FormControl(),
     TPer: new FormControl()
   });
  }
  selectedMain = '';
  selectedRel = '';
  words: Words[] = [];
  wordNames: string[] = [];
  mainWordRequest: MainWordRequest;
  relRequest: RelWordRequest;

  IsSideBarActive = false;

  IsAddMain = true;
  IsAddRel = false;

  CreateMainForm: FormGroup;
  CreateRelation: FormGroup;
  config = {
    search: true,
    placeholder: 'Виберіть',
    limitTo: 5,
    moreText: 'more',
    noResultsFound: 'Такого слова не знайдено',
    searchPlaceholder: 'Search',
    searchOnKey: 'name'
    };


  ngOnInit() {
    this.loadWords();
  }
  loadWords() {
    this.wordsService.getAll().subscribe((data: Words[]) => {
      if (data) {
       this.words = data.map(d => d);
       this.wordNames = data.map(d => d.word);
      }
    });
  }
  showSideBar() {
    if (this.IsSideBarActive) {
      this.IsSideBarActive = false;
    } else {
      this.IsSideBarActive = true;
    }
  }
  showAddMain() {
    this.IsAddMain = true;
    this.IsAddRel = false;
  }

  showAddRel() {
    this.IsAddMain = false;
    this.IsAddRel = true;
  }

  createMain() {
    this.mainWordRequest = {
      Word: this.CreateMainForm.get('addMain').value,
      Type: this.CreateMainForm.get('Type').value
    };
    this.wordsService.create(this.mainWordRequest).subscribe((result: any) => {
      this.alertify.success('Слово додано успішно');
    } );
    this.CreateMainForm.reset();
  }
  createRel() {
    this.relRequest = {
      wordId: this.words.find(d => d.word === this.selectedMain).id,
      wordRelId: this.words.find(d => d.word === this.selectedRel).id,
      malePoints: this.CreateRelation.get('MPoints').value,
      femalePoints: this.CreateRelation.get('FPoints').value,
      totalPoints: this.CreateRelation.get('TPoints').value,
      malePercents: this.CreateRelation.get('MPer').value,
      femalePercents: this.CreateRelation.get('FPer').value,
      totalPercents: this.CreateRelation.get('TPer').value
    };
    this.relService.create(this.relRequest).subscribe((result: any) => {
      this.alertify.success('Зв\'язок додано успішно');
    },
    (error) => {
      this.alertify.error('Такий зв\'язок вже існує, ви можете змінити його в розділі "слова"');
    } );
    this.CreateRelation.reset();
  }
}
