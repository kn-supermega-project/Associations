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

  constructor(private router: Router) { }

  ngOnInit() {
  }

  goToGraph() {
    this.router.navigate(['']);
  }
  goToWords() {
    this.router.navigate(['words']);
  }
  goToWordAdd() {
    this.router.navigate(['wordadd']);
  }
}
