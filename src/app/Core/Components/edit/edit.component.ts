import { Component, OnInit, Inject, Input } from '@angular/core';
import {MatDialogModule, MAT_DIALOG_DATA, MatDialog,MatDialogRef} from '@angular/material/dialog';
import { DisplayNotesComponent } from '../display-notes/display-notes.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { fbind } from 'q';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  form=new FormGroup({
    title : new FormControl(),
    Description : new FormControl(),
   });
 


  constructor(public dialog: MatDialogRef<EditComponent>, @Inject(MAT_DIALOG_DATA) public data) { }
   note;

  ngOnInit() {
    console.log(this.data);
    this.note=this.data;
  }
  Update(){
    console.log(this.form.value);
    this.dialog.close(FormData);
  }
  
}
