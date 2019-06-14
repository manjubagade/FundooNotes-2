import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserService } from '../../services/user.service';
import { NoteService } from '../../services/NoteService/note.service';
import * as jwt_decode from 'jwt-decode';
import { HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-mainnote',
  templateUrl: './mainnote.component.html',
  styleUrls: ['./mainnote.component.css']
})
export class MainnoteComponent implements OnInit {
  notes: any;
  CardNotes = [];
  id: string;
  @Output() public setNotes = new EventEmitter();
  UserId = localStorage.getItem('token');
  constructor(private service: NoteService) {
    var UserID = localStorage.getItem("UserId");
   
  }

  ngOnInit() {
   
     this.ViewCollaborators();
  }

  ViewCollaborators() {
    var UserId = localStorage.getItem("UserId");
    var t=localStorage.getItem('token');
    var headers_object = new HttpHeaders().set("Authorization", "Bearer " + t);

    this.service.getNotesById(UserId,headers_object).subscribe(
      data => {
        console.log(data);
        this.notes = data['result'];
       console.log(this.notes);
      }
    ), (err: any) => {
      console.log(err);
    };
  }
  closed(event) {
    console.log('event');
    this.ViewCollaborators();
  }
  
  eventOccur(event) {
    this.ViewCollaborators();
  }

}
