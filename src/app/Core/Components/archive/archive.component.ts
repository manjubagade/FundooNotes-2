import { Component, OnInit } from '@angular/core';
import { NoteService } from '../../services/NoteService/note.service';

@Component({
  selector: 'app-archive',
  templateUrl: './archive.component.html',
  styleUrls: ['./archive.component.css']
})
export class ArchiveComponent implements OnInit {
  notes = [];
  CardNotes = []
  id: string;
  more = 'isArchive'

  constructor(public notesService: NoteService) { }

  ngOnInit() {
    this.id = localStorage.getItem("userid")
    this.notesService.GetArchiveNotes(this.id).subscribe(
      (data: any) => {
        console.log(data);
        this.notes = data['result']
        this.notes.forEach(element => {
          if (element.IsArchive == true && element.IsTrash == false) {
            this.CardNotes.push(element)
            console.log(this.CardNotes, "notes");
          }
        });
      }
    ), (err: any) => {
      console.log(err);
};
  }

}
