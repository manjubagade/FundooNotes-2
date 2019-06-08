import { Component, OnInit, Inject, Input } from '@angular/core';
import { NoteService } from '../../services/NoteService/note.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { HttpHeaders } from '@angular/common/http';


var token=localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);

@Component({
  selector: 'app-label',
  templateUrl: './label.component.html',
  styleUrls: ['./label.component.css']
})
export class LabelComponent implements OnInit {
  Label:{
    labels,
    id
  }
  userid=localStorage.getItem('UserId');
  constructor(public dialog: MatDialogRef<LabelComponent>, @Inject(MAT_DIALOG_DATA) public data,public service:NoteService) { }
  
  ngOnInit() {
   
   this.Label = this.data;
  
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

  UpdateLabel(Label){
    console.log(Label.id);
    var Abc = {
     labels:Label.labels,
      id:Label.id
    }
    console.log(">>>>>>>>>>>>>>>>>>>>>>rr",Abc);
    console.log("In LAbel Component" ,Label);
    this.service.UpdateLabel(Abc,Label.id,headers_object).subscribe(data=>{

    })
  }
  UpdateAllLabel(Label){
    console.log(Label);
    var last = Label.length-1;
    console.log(last);
    console.log("Last"+Label[last]);
    console.log("pppppp"+this.Label);
    
    // console.log(">>>>>>>>>>>>>>>>>>>>>>rr",Abc);
    // console.log("In LAbel Component" ,Label);
    this.service.UpdateLabel(Label[last],Label[2].id,headers_object).subscribe(data=>{
      this.dialog.close();
    })
  }

  DeleteLabel(id){
    console.log("?????In LAbel Component" ,id);
    this.service.deleteLabel(id,headers_object).subscribe(data=>{
      console.log(data);
    });
  }
}