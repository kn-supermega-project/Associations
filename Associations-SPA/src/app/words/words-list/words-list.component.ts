import { Component, OnInit, TemplateRef } from '@angular/core';
import { WordsService } from 'src/app/_services/words.service';
import { WordsToList } from 'src/app/_interfaces/WordsToList';
import { PaginationQuery } from 'src/app/_interfaces/PaginationQuery';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MainWordRequest } from 'src/app/RequestModels/MainWord-request';

@Component({
  selector: 'app-words-list',
  templateUrl: './words-list.component.html',
  styleUrls: ['./words-list.component.css']
})
export class WordsListComponent implements OnInit {

  wordsToList: WordsToList[];
  pagination: PaginationQuery;
  totalRecords: number[];
  modalRef: BsModalRef;
  newWord: string;
  wordRequest: MainWordRequest;
  templateEdit: TemplateRef<any>;

  constructor(private wordsService: WordsService,
    private router: Router,
    private modalService: BsModalService) {
    this.pagination = this.getDefaultPaginationParam();
    this.totalRecords = [];
   }

  ngOnInit() {
    this.loadWords();
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

    this.loadWords();
  }

  loadWords() {
    this.wordsService.getMainWords(this.pagination.pageSize, this.pagination.pageNumber)
      .subscribe((response) => {
        this.wordsToList = response.body;
        const totalPages = JSON.parse(response.headers.get('X-Pagination')).TotalPages;
        if (this.totalRecords = []) {
          this.totalRecords = Array(totalPages).fill(1).map((x, i) => i + 1);
        }
      });
  }
  details(id: number) {
    this.router.navigate(['words/', id]);
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  EditWord(wordId: number, wordType: number) {
    if (this.newWord) {
      this.wordRequest.Type = wordType;
      this.wordRequest.Word = this.newWord;
      this.wordsService.update(wordId, this.wordRequest).
      subscribe(data => this.loadWords);
      this.modalRef.hide();
    }
  }

  OpenIdModal(id: number) {
    this.openModal(this.templateEdit);
  }
}
