import { Component, OnInit, Inject, Input } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialog,MatDialogRef } from '@angular/material/dialog';
import { DisplayNotesComponent } from '../display-notes/display-notes.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { fbind } from 'q';
import { UserService } from '../../services/user.service';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(public dialog: MatDialogRef<EditComponent>, @Inject(MAT_DIALOG_DATA) public data,public service:NoteService) { }
   note;
   title;
   description;
   image;

  ngOnInit() {
    console.log(this.data);
    this.note=this.data;
    this.title=this.note.title;
    this.description=this.note.description;
    this.image=this.note.image;
    
  }
  Update(){
   
    console.log(this.data);
    this.image=this.note.image=null;
    if(this.title!==this.note.title ||  this.description!=this.note.description || this.image!=this.note.image){
      console.log("it entered here");
      this.dialog.close('change');
    }
    this.dialog.close();
  }
 
}