import { Component, OnInit } from '@angular/core';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-trash',
  templateUrl: './trash.component.html',
  styleUrls: ['./trash.component.css']
})
export class TrashComponent implements OnInit {
  more="IsTrash";
  notes = [];
  CardNotes = []
  id: string;
  constructor(public notesService: NoteService) { }

  ngOnInit() {
    this.id = localStorage.getItem("UserId")
    console.log("ssssss0"+this.id);
    var t=localStorage.getItem('token');
  var headers_object = new HttpHeaders().set("Authorization", "Bearer " + t);
    this.notesService.ViewInTrash(this.id,headers_object).subscribe(
      (data: any) => {
        console.log(data);
        this.notes = data['result']
        console.log(this.notes);
        
        this.notes.forEach(element => {
          if (element.IsTrash == true) {
            this.CardNotes.push(element);
            console.log(this.CardNotes, "notes");
          }
        });
      }
    ), (err: any) => {
      console.log(err);
    };
}
  }

