import { Component, OnInit } from '@angular/core';
import { WordsService } from '../_services/words.service';
import { Words } from '../_interfaces/Words';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  selected: string;
  words: string[] = [];

  constructor(private wordsService: WordsService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

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
  goToGraph() {
    this.router.navigate(['']);
  }
  goToWords() {
    this.router.navigate(['words']);
  }

}
