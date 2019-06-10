import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { Console } from '@angular/core/src/console';
import { UserComponent } from '../user/user.component';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { EditComponent } from '../edit/edit.component';
import { IconComponent } from '../icon/icon.component';
import { NoteService } from '../../services/NoteService/note.service';
import { DataService } from '../../services/DataService/data.service';
import { MatChipInputEvent } from '@angular/material';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { notEqual } from 'assert';


var token=localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);

@Component({
  selector: 'app-display-notes',
  templateUrl: './display-notes.component.html',
  styleUrls: ['./display-notes.component.css'],
  
})
export class DisplayNotesComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  pipe;
  addOnBlur = true;
  separatorKeysCodes = [ENTER, COMMA];
  
  constructor(private service: NoteService, private dataService: DataService, route: Router,
    private toastr: ToastrService, public dialog: MatDialog) {

  }
  grid;
  @Input() search;
  @Input() cards;
  @Input() archived;
  @Input() trash;
  @Input() untrash;
  @Output() messageEvent = new EventEmitter<any>();
abc;
  flag = true;
  unrchive: boolean;
  archive: boolean;
  trashNote: boolean;
  css = 'row wrap'
  ngOnInit() {
    console.log("***********"+this.cards);
    
    
    var Profilepic = localStorage.getItem("profilePic");
    console.log("Display Profile pic", Profilepic);
    var UserId=localStorage.getItem('UserId');
    var profile = localStorage.getItem('profilePic');

    this.dataService.currentMessage.subscribe(data => {
      console.log(data);
      console.log(this.css);

      this.css = data ? 'row wrap' : 'column'

      this.flag = data;
      
      
    });

    this.service.ViewCollaborators(UserId,headers_object).subscribe(data=>{
      console.log(data);
      this.abc=data;
    })
    

  }

 
  openDialog(note) {
    console.log(note);
    // const dialogRef = this.dialog.open(EditNotes);
    console.log("opendialog");
    
    const dialogRef = this.dialog.open(EditComponent,
      {
        data: note,
      });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'change') {
        console.log("take action here");
      }
      else {
        console.log("execute");
        console.log(note);

        console.log(note.id);
        
        this.service.UpdateNotes(note.id,note,headers_object).subscribe(data => {
          console.log(data);
          // this.Delete.emit({});
        }, err => {
          console.log(err);
        })
      }
    })
  }

  Archive(event) {
    console.log('event here');
    this.messageEvent.emit(event)
  }

  /**
   * 
   * @param event 
   */
  Trash(event) {
    console.log('trash in');
    this.messageEvent.emit(event);

}

Untrash(event){
  console.log('trash in');
    this.messageEvent.emit(event);
}

  remove(id,note) {
    
    note.label = null;
    console.log("00000000000" + note.label, id);
    this.service.UpdateNotes(id,note,headers_object).subscribe(data => {
      console.log("88888888" + note, id)
    })
  }
  Pin(card){
    card.Pin=true;
    console.log(card.id,card);
    
    this.service.UpdateNotes(card,card.id,headers_object).subscribe(data=>{

    })
  }
  
  Reminder(id,note) {
    console.log(note);
    
    note.reminder = '0001-01-01T00:00:00';
    console.log(note);
    
    console.log("00000000000" + id, note);
    this.service.UpdateNotes(id,note,headers_object).subscribe(data => {
      console.log("88888888" + note, id)
    })
  }
  
}