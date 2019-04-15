import { Component, OnInit } from '@angular/core';
import { WordRelsToList } from 'src/app/_interfaces/WordRelsToList';
import { WordRelsService } from 'src/app/_services/WordRels.service';
import { PaginationQuery } from 'src/app/_interfaces/PaginationQuery';
import { ActivatedRoute } from '@angular/router';
import { WordsToList } from 'src/app/_interfaces/WordsToList';
import { WordsService } from 'src/app/_services/words.service';

@Component({
  selector: 'app-words-details',
  templateUrl: './words-details.component.html',
  styleUrls: ['./words-details.component.css']
})
export class WordsDetailsComponent implements OnInit {

  relWords: WordRelsToList[];
  pagination: PaginationQuery;
  totalRecords: number[];
  mainWord: WordsToList;
  mainWordId: number;


  constructor(private wordrelsService: WordRelsService,
    private router: ActivatedRoute,
    private wordsService: WordsService) {
    this.pagination = this.getDefaultPaginationParam();
    this.totalRecords = [];
    this.router.params.subscribe(params => {
      this.mainWordId = params['id'];
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

    this.loadRelWords(this.mainWord.id);
  }
  loadRelWords(id: number) {
    this.wordrelsService.getRelWordsByMainId(id, this.pagination.pageSize, this.pagination.pageNumber).subscribe((response) => {
        this.relWords = response.body;
        const totalPages = JSON.parse(response.headers.get('X-Pagination')).TotalPages;
        if (this.totalRecords = []) {
          this.totalRecords = Array(totalPages).fill(1).map((x, i) => i + 1);
        }
      }
    );
  }
  loadContent() {
    this.wordsService.getById(this.mainWordId).subscribe((response: WordsToList) => {
      if (response) {
        this.mainWord = response;
      }
      this.loadRelWords(this.mainWord.id);
    });
  }

}
