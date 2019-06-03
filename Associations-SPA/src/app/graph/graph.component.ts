import { Component, OnInit, ViewChild } from '@angular/core';
import * as shape from 'd3-shape';
import { WordsRel } from '../_interfaces/WordsRel';
import { WordsService } from '../_services/words.service';
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { Words } from '../_interfaces/Words';


@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {

  hierarchialGraph = {nodes: [], links: [] };
  public curve: any = shape.curveBundle.beta(1);
  words: Words[];
  IsSideBarActive = false;
  isTotal = true; // as default
  isMale = false;
  isFemale = false;

  constructor(private wordsService: WordsService) {}

  ngOnInit() {
    this.showGraph();
  }

  showGraph() {
      this.wordsService.getAll()
       .subscribe((data: Words[]) => {
         if (data) {
          this.words = data;
         }
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
                    model: {
                     totalLabel: 'Загально: ' + rel.totalPoints.toString(),
                     maleLabel: 'Чоловічі: ' + rel.malePoints.toString(),
                     femaleLabel: 'Жіночі: ' + rel.femalePoints.toString()
                    }
                  });
              });
          });
       });
  }

  showTotal() {
    this.isTotal = true; // as default
    this.isMale = false;
    this.isFemale = false;
  }
  showMale() {
    this.isTotal = false;
    this.isMale = true;
    this.isFemale = false;
  }
  showFemale() {
    this.isTotal = false;
    this.isMale = false;
    this.isFemale = true;
  }

  showSideBar() {
    if (this.IsSideBarActive) {
      this.IsSideBarActive = false;
    } else {
      this.IsSideBarActive = true;
    }
  }

  // not good idea
  // tslint:disable-next-line:use-life-cycle-interface
    ngAfterViewInit(): void {
      setTimeout(_ => {
      window.dispatchEvent(new Event('resize'));
      }); // BUGFIX:
    }
}
