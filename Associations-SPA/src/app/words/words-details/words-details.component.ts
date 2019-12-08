import { Component, OnInit, TemplateRef } from '@angular/core';
import { WordRelsToList } from 'src/app/_interfaces/WordRelsToList';
import { WordRelsService } from 'src/app/_services/WordRels.service';
import { PaginationQuery } from 'src/app/_interfaces/PaginationQuery';
import { ActivatedRoute } from '@angular/router';
import { WordsService } from 'src/app/_services/words.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Words } from 'src/app/_interfaces/Words';
import { FormGroup, FormControl } from '@angular/forms';
import { RelWordRequest } from 'src/app/RequestModels/RelWord-requst';
import { AlertifyService } from 'src/app/_services/alertify.service';


@Component({
  selector: 'app-words-details',
  templateUrl: './words-details.component.html',
  styleUrls: ['./words-details.component.css']
})
export class WordsDetailsComponent implements OnInit {

  relWords: WordRelsToList[];
  pagination: PaginationQuery;
  totalRecords: number[];
  mainWord: Words;
  mainWordId: number;
  modalRef: BsModalRef;
  activeItem = 1;
  RelRequest: RelWordRequest;
  searchString: string;

  RelId: number;

  OldWord: Words;
  ChangeRelation: FormGroup;

  constructor(private wordrelsService: WordRelsService,
    private router: ActivatedRoute,
    private wordsService: WordsService,
    private modalService: BsModalService,
    private alertify: AlertifyService) {
    this.pagination = this.getDefaultPaginationParam();
    this.totalRecords = [];
    this.router.params.subscribe(params => {
      this.mainWordId = params['id'];
    });
    this.ChangeRelation = new FormGroup({
      MPoints: new FormControl(),
      FPoints: new FormControl(),
      TPoints: new FormControl(),
      MPer: new FormControl(),
      FPer: new FormControl(),
      TPer: new FormControl()
    });
  }

  ngOnInit() {
    this.loadContent();
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
    this.loadRelWords(this.mainWord.id);
  }
  loadRelWords(id: number) {
    this.wordrelsService.getByFilter(id, this.searchString, this.pagination.pageSize, this.pagination.pageNumber).subscribe((response) => {
        this.relWords = response.body;
        const totalPages = JSON.parse(response.headers.get('X-Pagination')).TotalPages;
        if (this.totalRecords = []) {
          this.totalRecords = Array(totalPages).fill(1).map((x, i) => i + 1);
        }
      }
    );
  }
  loadContent() {
    this.wordsService.getById(this.mainWordId).subscribe((response: Words) => {
      if (response) {
        this.mainWord = response;
      }
      this.loadRelWords(this.mainWord.id);
    });
  }

  loadWordById(id: number) {
    this.wordsService.getById(id)
    .subscribe((response: Words) => {
      this.OldWord = response;
    });
  }

  openModal(template: TemplateRef<any>, id: number, RelId: number) {
    this.loadWordById(id);
    this.RelId = RelId;
    this.modalRef = this.modalService.show(template);
    this.setDefaultRequest();
  }
  setDefaultRequest() {
    this.RelRequest = {
      wordId: null,
      wordRelId: null,
      malePoints: null,
      femalePoints: null,
      totalPoints: null,
      malePercents: null,
      femalePercents: null,
      totalPercents: null
    };
  }

  EditWord() {
    if (this.RelId &&
      this.OldWord.id &&
      this.ChangeRelation.get('MPoints').value &&
      this.ChangeRelation.get('FPoints').value &&
      this.ChangeRelation.get('TPoints').value &&
      this.ChangeRelation.get('MPer').value &&
      this.ChangeRelation.get('FPer').value &&
      this.ChangeRelation.get('TPer').value) {

      this.RelRequest = {
      wordId: this.mainWord.id,
      wordRelId: this.OldWord.id,
      malePoints: this.ChangeRelation.get('MPoints').value,
      femalePoints: this.ChangeRelation.get('FPoints').value,
      totalPoints: this.ChangeRelation.get('TPoints').value,
      malePercents: this.ChangeRelation.get('MPer').value,
      femalePercents: this.ChangeRelation.get('FPer').value,
      totalPercents: this.ChangeRelation.get('TPer').value
      };
      this.RelRequest = {
      wordId: this.mainWord.id,
      wordRelId: this.OldWord.id,
      malePoints: this.ChangeRelation.get('MPoints').value,
      femalePoints: this.ChangeRelation.get('FPoints').value,
      totalPoints: this.ChangeRelation.get('TPoints').value,
      malePercents: this.ChangeRelation.get('MPer').value,
      femalePercents: this.ChangeRelation.get('FPer').value,
      totalPercents: this.ChangeRelation.get('TPer').value
      };

      this.wordrelsService.update(this.RelId, this.RelRequest)
      .subscribe(() => {
        this.modalRef.hide();
        this.loadContent();
        this.ChangeRelation.reset();
        this.alertify.success('Деталі зв\'язку змінено успішно');
      });
    }
  }

  deleteWord() {
    if (this.RelId) {
      this.wordrelsService.delete(this.RelId)
      .subscribe(() => {
        this.modalRef.hide();
        this.loadContent();
        this.alertify.message('Зв\'язок видалено успішно');
      });
    }
  }

  search() {
    this.loadContent();
  }

}
