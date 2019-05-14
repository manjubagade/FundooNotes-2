import { Component, OnInit, Inject } from '@angular/core';
import { NoteService } from '../../services/NoteService/note.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-label',
  templateUrl: './label.component.html',
  styleUrls: ['./label.component.css']
})
export class LabelComponent implements OnInit {
 Label;
  constructor(public dialog: MatDialogRef<LabelComponent>, @Inject(MAT_DIALOG_DATA) public data,public service:NoteService) { }

  ngOnInit() {
   this.Label =this.data;
    console.log("///////////",this.Label);
    // this.title=this.note.title;
    // this.description=this.note.description;
    // this.image=this.note.image;
  }
  AddLabel(Label){
    
    console.log(">>>>>>>>>>>>>>>>>>>>>>",Label);
    console.log("In LAbel Component" ,Label);
    this.dialog.close(Label);
  }
  DeleteLabel(id){
    console.log("?????In LAbel Component" ,id);
    this.service.deleteLabel(id).subscribe(data=>{
      console.log(data);
    });
  }
}