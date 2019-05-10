import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { NoteService } from '../../services/NoteService/note.service';

@Component({
  selector: 'app-mainnote',
  templateUrl: './mainnote.component.html',
  styleUrls: ['./mainnote.component.css']
})
export class MainnoteComponent implements OnInit {
  notes: any;
 id: string;

  constructor(private service:NoteService) { 
    var UserID = localStorage.getItem("UserId");
    console.log(UserID);
  }
 
  ngOnInit() {
    var UserID = localStorage.getItem("UserId");
    this.service.getNotesById(UserID).subscribe(
      data => {
        this.notes = data;
      }
    ), (err: any) => {
      console.log(err);
   };
  }

}
