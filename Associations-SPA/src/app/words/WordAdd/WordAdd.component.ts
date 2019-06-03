import { Component, OnInit } from '@angular/core';
import { WordsService } from 'src/app/_services/words.service';
import { Words } from 'src/app/_interfaces/Words';
import { FormGroup, FormControl } from '@angular/forms';
import { MainWordRequest } from 'src/app/RequestModels/MainWord-request';
import { WordsRel } from 'src/app/_interfaces/WordsRel';
import { RelWordRequest } from 'src/app/RequestModels/RelWord-requst';
import { WordRelsService } from 'src/app/_services/WordRels.service';

@Component({
  selector: 'app-WordAdd',
  templateUrl: './WordAdd.component.html',
  styleUrls: ['./WordAdd.component.css']
})
export class WordAddComponent implements OnInit {
  selectedMain: string;
  selectedRel: WordsRel;
  words: Words[] = [];
  relWords: WordsRel[] = [];
  mainWordRequest: MainWordRequest;
  relRequest: RelWordRequest;

  IsSideBarActive = false;

  IsAddMain = true;
  IsAddRel = false;

  CreateMainForm: FormGroup;
  CreateRelation: FormGroup;

  constructor(private wordsService: WordsService, private relService: WordRelsService) {
    this.CreateMainForm = new FormGroup({
      addMain: new FormControl(),
      Type: new FormControl()
   });
   this.CreateRelation = new FormGroup({
     MainWord: new FormControl(),
     RelWord: new FormControl(),
     MPoints: new FormControl(),
     FPoints: new FormControl(),
     TPoints: new FormControl(),
     MPer: new FormControl(),
     FPer: new FormControl(),
     TPer: new FormControl()
   });
  }

  ngOnInit() {
    this.loadMainWords();
  }
  loadMainWords() {
    this.wordsService.getAll().subscribe((data: Words[]) => {
      if (data) {
       this.words = data.map(d => d);
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
    } );
    this.CreateMainForm.reset();
  }
  createRel() {
    this.relRequest = {
      wordId: this.CreateRelation.get('MainWord').value,
      wordRelId: this.CreateRelation.get('RelWord').value,
      malePoints: this.CreateRelation.get('MPoints').value,
      femalePoints: this.CreateRelation.get('FPoints').value,
      totalPoints: this.CreateRelation.get('TPoints').value,
      malePercents: this.CreateRelation.get('MPer').value,
      femalePercents: this.CreateRelation.get('FPer').value,
      totalPercents: this.CreateRelation.get('TPer').value
    };
    this.relService.create(this.relRequest).subscribe((result: any) => {
    } );
    this.CreateRelation.reset();
  }

}
