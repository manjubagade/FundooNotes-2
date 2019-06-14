import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { Console } from '@angular/core/src/console';
import { UserComponent } from '../user/user.component';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialog, MatDialogRef, _countGroupLabelsBeforeOption } from '@angular/material';
import { EditComponent } from '../edit/edit.component';
import { IconComponent } from '../icon/icon.component';
import { NoteService } from '../../services/NoteService/note.service';
import { DataService } from '../../services/DataService/data.service';
import { MatChipInputEvent } from '@angular/material';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { notEqual } from 'assert';
import { DragDropModule, moveItemInArray, CdkDragDrop } from '@angular/cdk/drag-drop';

var token = localStorage.getItem('token');
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
  LabelOnNotes:any;
  addOnBlur = true;
  timePeriods = [
    'Bronze age',
    'Iron age',
    'Middle ages',
    'Early modern period',
    'Long nineteenth century'
  ];
  separatorKeysCodes = [ENTER, COMMA];
  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.timePeriods, event.previousIndex, event.currentIndex);
  }
  constructor(private service: NoteService, private dataService: DataService, route: Router,
    private toastr: ToastrService, public dialog: MatDialog) {
// this.cardLabel=this.cardLabel;      
// console.log(this.cardLabel);

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
label:any;
  unrchive: boolean;
  archive: boolean;
  trashNote: boolean;
  Label:any;
  cardLabel:any;
  css = 'row wrap'
  flag1 = true;
  
  ngOnInit() {
    var Profilepic = localStorage.getItem("profilePic");

    var UserId = localStorage.getItem('UserId');
    var profile = localStorage.getItem('profilePic');

    this.dataService.currentMessage.subscribe(data => {


      this.css = data ? 'row wrap' : 'column'

      this.flag = data;


    });
    this.service.viewNotesLabel(UserId, headers_object).subscribe(data=>{
      this.cardLabel=data['result'];
      
      console.log(this.cardLabel,"labeldata");

    })
    console.log(this.cardLabel,"labeldata123");
    
    

    this.service.ViewCollaborators(UserId, headers_object).subscribe(data => {

      this.abc = data;
    })
    this.service.getLabelsById(UserId,headers_object).subscribe(
      data => {
        this.Label = data;
       
        console.log(this.Label,"aniket");

      }

    ), (err: any) => {
      console.log(err);
    };


  }


  openDialog(note) {



    const dialogRef = this.dialog.open(EditComponent,
      {
        data: note,
        panelClass: 'updateDialog'
      });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'change') {

      }
      else {
        this.service.UpdateNotes(note.id, note, headers_object).subscribe(data => {

        }, err => {
          console.log(err);
        })
      }
    })
  }

  Archive(event) {

    this.messageEvent.emit(event)
  }

  /**
   * 
   * @param event 
   */
  Trash(event) {

    this.messageEvent.emit(event);

  }

  Untrash(event) {

    this.messageEvent.emit(event);
  }

  remove(id) {
    this.service.DeleteNotesLabel(id).subscribe(data => {
      this.messageEvent.emit(event);
    })
  }
 
  Pin(card) {
 
    card.Pin = true;


    this.service.UpdateNotes(card.id, card, headers_object).subscribe(data => {

    })
  }
  UnPin(card) {
    card.Pin = false;
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(data => {

    })
  }

  Reminder(id, note) {
    note.reminder = '0001-01-01T00:00:00';
    this.service.UpdateNotes(id, note, headers_object).subscribe(data => {

    })
  }
  receiveMessage($event) {
    this.Label = $event
    console.log(this.Label,"Inserting here");
    
  }
}