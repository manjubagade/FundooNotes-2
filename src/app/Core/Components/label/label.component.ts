import { Component, OnInit, Inject } from '@angular/core';
import { NoteService } from '../../services/NoteService/note.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-label',
  templateUrl: './label.component.html',
  styleUrls: ['./label.component.css']
})
export class LabelComponent implements OnInit {
// Label;
  constructor(public dialog: MatDialogRef<LabelComponent>, @Inject(MAT_DIALOG_DATA) public data,public service:NoteService) { }

  ngOnInit() {
    // this.Label=this.Label;
  }
  AddLabel(label){
    console.log(">>>>>>>>>>>>>>>>>>>>>>",label);
    
    console.log("In LAbel Component" ,label);
    this.dialog.close(label);
  }
}