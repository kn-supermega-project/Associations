import { Component, OnInit, ViewChild } from '@angular/core';
import * as shape from 'd3-shape';
import { WordsRel } from '../_interfaces/WordsRel';
import { WordsService } from '../_services/words.service';
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { Words } from '../_interfaces/Words';


@Component({
  selector: 'app-total-graph',
  templateUrl: './total-graph.component.html',
  styleUrls: ['./total-graph.component.css']
})
export class TotalGraphComponent implements OnInit {

  hierarchialGraph = {nodes: [], links: [] };
  public curve: any = shape.curveBundle.beta(1);
  words: Words[];

  constructor(private wordsService: WordsService) {}

  ngOnInit() {
    this.loadWordsRels();
  }

  loadWordsRels() {
      this.wordsService.getAll()
       .subscribe((data: Words[]) => {
          this.words = data;
          let i = 1;
          this.words.forEach(word => {
            this.hierarchialGraph.nodes.push(
                {
                  id: word.id.toString(),
                  label: word.word.toString(),
                  position: ('x' + i).toString()
                });
                i++;
              word.rels.forEach(rel => {
                this.hierarchialGraph.links.push(
                  {
                    source: word.id.toString(),
                    target: rel.wordRelId.toString(),
                    label: 'Total Points: ' + rel.totalPoints.toString()
                  });
              });
          });
       });
  }

  // not good idea

  // tslint:disable-next-line:use-life-cycle-interface
    ngAfterViewInit(): void {
      setTimeout(_ => {
      window.dispatchEvent(new Event('resize'));
      }); // BUGFIX:
    }
}
