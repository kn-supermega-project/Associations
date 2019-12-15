import { Component, OnInit, TemplateRef } from '@angular/core';
import { WordsService } from 'src/app/_services/words.service';
import { PaginationQuery } from 'src/app/_interfaces/PaginationQuery';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MainWordRequest } from 'src/app/RequestModels/MainWord-request';
import { Words } from 'src/app/_interfaces/Words';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-words-list',
  templateUrl: './words-list.component.html',
  styleUrls: ['./words-list.component.css']
})
export class WordsListComponent implements OnInit {

  wordsToList: Words[];
  pagination: PaginationQuery;
  totalRecords: number[];
  modalRef: BsModalRef;
  newWord: string;
  wordRequest: MainWordRequest;
  activeItem = 1;
  searchString: string;


  OldWord: Words;

  constructor(private wordsService: WordsService,
    private router: Router,
    private modalService: BsModalService,
    private alertify: AlertifyService) {
    this.pagination = this.getDefaultPaginationParam();
    this.totalRecords = [];
   }

  ngOnInit() {
    this.loadWords();
  }
  setDefaultRequest() {
    this.wordRequest = {
      Type: 0,
      Word: ''
    };
  }
  getDefaultPaginationParam(): PaginationQuery {
    return {
      pageSize: 10,
      pageNumber: 1
    };
  }
  paginate(p: number) {
    this.pagination = {
      pageNumber: p,
      pageSize: 10
    };
    this.activeItem = p;
    this.loadWords();
  }

  loadWords() {
    this.wordsService.getByFilter(this.searchString, this.pagination.pageSize, this.pagination.pageNumber)
      .subscribe((response) => {
        this.wordsToList = response.body;
        const totalPages = JSON.parse(response.headers.get('X-Pagination')).TotalPages;
        if (this.totalRecords = []) {
          this.totalRecords = Array(totalPages).fill(1).map((x, i) => i + 1);
        }
      });
  }
  loadWordById(id: number) {
    this.wordsService.getById(id)
    .subscribe((response: Words) => {
      this.OldWord = response;
    });
  }
  details(id: number) {
    this.router.navigate(['word-details/', id]);
  }
  openModal(template: TemplateRef<any>, id: number) {
    this.loadWordById(id);
    this.modalRef = this.modalService.show(template);
    this.setDefaultRequest();

  }
  EditWord() {
    if (this.newWord && this.OldWord.id) {
      this.wordRequest.Word = this.newWord;
      this.wordRequest.Type = 1;
      this.wordsService.update(this.OldWord.id, this.wordRequest)
      .subscribe(() => {
        this.modalRef.hide();
        this.loadWords();
        this.newWord = '';
        this.alertify.success('Слово змінено успішно');
      });
    }
  }
  deleteWord() {
    if (this.OldWord.id) {
      this.wordsService.delete(this.OldWord.id)
      .subscribe(() => {
        this.modalRef.hide();
        this.loadWords();
        this.alertify.message('Слово видалено');
      },
      (error) => {
        this.alertify.error('Не можливо видалити слово, якщо воно є асоціацією іншого. Спочатку видаліть цей зв\'язок');
      });
    }
  }
  setDefaultWordsList() {
    this.wordsToList = [];
  }

  search() {
      this.loadWords();
  }

}
