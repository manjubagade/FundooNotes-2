import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserService } from '../../services/user.service';
import { NoteService } from '../../services/NoteService/note.service';
import * as jwt_decode from 'jwt-decode';
@Component({
  selector: 'app-mainnote',
  templateUrl: './mainnote.component.html',
  styleUrls: ['./mainnote.component.css']
})
export class MainnoteComponent implements OnInit {
  notes: any;
  CardNotes =[];
 id: string;
 @Output() public setNotes=new EventEmitter();
 UserId=localStorage.getItem('token');
  constructor(private service:NoteService) { 
    var UserID = localStorage.getItem("UserId");
    console.log("Main "+UserID);
  }
 
  ngOnInit() {
    console.log("Im In Main Component");
    
   var result= this.getAllNotes();
  }

  getAllNotes(){
    // this.CardNotes = [];
    var UserId=localStorage.getItem("UserId");
   
    
    this.service.getNotesById(UserId).subscribe(
      data => {
        this.notes = data;
        console.log(data);
        
//         for (let i = 0; i < this.notes.length; i++) {
//           if (this.notes[i].isArchive == false && this.notes[i].isTrash == false) {
//             this.CardNotes.push(this.notes[i])
// }
//         this.setNotes.emit(this.getAllNotes);
//       }
    }
    ), (err: any) => {
      console.log(err);
   };
  }
  closed(event){
    console.log('event');
     this.getAllNotes();
    }
    eventOccur(event) {
      this.getAllNotes();
    }
  
}
