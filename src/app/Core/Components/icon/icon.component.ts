import { Component, OnInit, Input, Inject, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { MatDialogRef, MAT_DIALOG_DATA, mixinColor } from '@angular/material';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-icon',
  templateUrl: './icon.component.html',
  styleUrls: ['./icon.component.css']
})
export class IconComponent implements OnInit {
  public progress: number;
  public message: string;
  Label;
 flag=true
  @Output() public onUploadFinished = new EventEmitter();
  @Output() public setColor=new EventEmitter();
  constructor(private route:Router,private service:NoteService,private http:HttpClient) { 
    this.Label = this.Label;
    console.log("gjhgj"+this.Label);
  }
  @Input() cards;

   color;
  ngOnInit() {
    var UserID=localStorage.getItem('UserId');
    this.service.getLabelsById(UserID).subscribe(
      data => {
        this.Label = data;
      }

    ), (err: any) => {
      console.log(err);
    };
  }

  Delete(card)
{
card.delete = true;
card.IsTrash = card.delete;
this.service.DeleteNote(this.cards.id).subscribe(data =>{
console.log(data);
},err =>{
console.log(err);
})
}

public uploadFile = (files) => {
  if (files.length === 0) {
    console.log(files);
   return }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    var id=this.cards.id;
    this.http.post(environment.BaseURI+'/Notes/image/'+id, formData, {reportProgress: true, observe: 'events'})
    .subscribe(event => {
      console.log(formData);
      
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response) {
        this.message = 'Upload success.';
        this.onUploadFinished.emit(event.body);
      }
    });
  }
  // setcolor(color,cards){
  //   this.service.SetColor(color,this.cards);
  // }
  setcolor(color,cards)
{
  this.cards.color=color;
  
this.service.SetColor(this.cards,this.cards.id).subscribe(data =>{
this.setColor.emit(this.setcolor);
},err =>{
console.log(err);
})
}
checkBox(label){
  this.Label.labels=label;
  // this.service.
  console.log("After Check"+this.Label.labels);
}
}