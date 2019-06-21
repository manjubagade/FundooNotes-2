import { Component, OnInit, Inject, Input } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialog,MatDialogRef } from '@angular/material/dialog';
import { DisplayNotesComponent } from '../display-notes/display-notes.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { fbind } from 'q';
import { UserService } from '../../services/user.service';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpHeaders } from '@angular/common/http';

var token = localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(public dialog: MatDialogRef<EditComponent>, @Inject(MAT_DIALOG_DATA) public data,public service:NoteService) { }
   note;
   title;
   collaborator;
   description;
   image;
   Label;
   cardLabel;

  ngOnInit() {
    console.log(this.data);
    this.note=this.data;

console.log(this.collaborator);

    this.title=this.note.title;
    this.description=this.note.description;
    this.image=this.note.image;
    var UserId = localStorage.getItem('UserId');

    this.service.viewNotesLabel(UserId,headers_object).subscribe(data=>{
      this.cardLabel=data['result'];
      console.log(this.cardLabel,"labeldata");

      this.service.getLabelsById(UserId,headers_object).subscribe(
        data => {
          this.Label = data;
         
          console.log(this.Label,"aniket");
  
        }
  
      ), (err: any) => {
        console.log(err);
      };
  

    })
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