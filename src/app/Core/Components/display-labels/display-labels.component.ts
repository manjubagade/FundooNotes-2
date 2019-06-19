import { Component, OnInit } from '@angular/core';
import { NoteService } from '../../services/NoteService/note.service';

@Component({
  selector: 'app-display-labels',
  templateUrl: './display-labels.component.html',
  styleUrls: ['./display-labels.component.css']
})
export class DisplayLabelsComponent implements OnInit {

  constructor(private service:NoteService) { }

  ngOnInit() {
    // this.service.displayLabels.subscribe()
  }

}
