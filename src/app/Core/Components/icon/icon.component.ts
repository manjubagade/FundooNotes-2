import { Component, OnInit, Input, Inject, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import {  MatDialogRef, MAT_DIALOG_DATA, mixinColor, MatDialog } from '@angular/material';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpClient, HttpEventType, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CollaboratorsComponent } from '../Collaborators/collaborators.component';
import { EditComponent } from '../edit/edit.component';

var token=localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);

@Component({
  selector: 'app-icon',
  templateUrl: './icon.component.html',
  styleUrls: ['./icon.component.css']
  
})
export class IconComponent implements OnInit {
  public progress: number;
  public message: string;
  Label;
  Email;
  reminder;
 flag=true
  @Output() public onUploadFinished = new EventEmitter();
  @Output() public setColor=new EventEmitter();
  
  @Input() archivedicon
  @Input() trashed
  @Output() setNote = new EventEmitter();

trash: boolean = true;
archive: boolean = true;
unarchive: boolean = true;

  constructor(private route:Router,private service:NoteService,private http:HttpClient,public dialog: MatDialog) { 
    this.Label = this.Label;
    console.log("gjhgj"+this.Label);
  }
  @Input() cards;
 
   color;

  ngOnInit() {
    console.log("In Icon Components"+this.cards);
    console.log("Trash="+this.trashed);
    
    
    var UserID=localStorage.getItem('UserId');
    
    this.service.getLabelsById(UserID,headers_object).subscribe(
      data => {
        this.Label = data;
      }

    ), (err: any) => {
      console.log(err);
    };
    // console.log("In Icon"+this.cards);
  }


  Delete(card)
{

this.service.DeleteNote(this.cards.id).subscribe(data =>{
console.log(data);
this.setNote.emit(data);
},err =>{
console.log(err);
})
}

public uploadFile = (files) => {
  if (files.length === 0) {
    console.log(files);
   return
   }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    var id=this.cards.id;
    this.service.AddImage(id, formData).subscribe(data=>{
      this.setColor.emit(this.uploadFile);
     console.log(data);
    })
    
    
  }
  
  setcolor(color,cards)
{
  this.cards.color=color;
this.service.UpdateNotes(this.cards.id,this.cards,headers_object).subscribe(data =>{
this.setColor.emit(this.setcolor);

},err =>{
console.log(err);
})
}

Archive(card) {
  card.IsArchive = true;
  console.log(card)
  this.service.UpdateNotes(card.id, card,headers_object).subscribe(
    data => {
      console.log(data);
      this.setNote.emit(card);
    },
    err => { console.log(err); }
  )
}

/**
 * 
 * @param card 
 */
Unarchive(card) {
  card.isArchive = false;
  this.service.UpdateNotes(card.id, card,headers_object).subscribe(
    data => {
      console.log(data);
      this.setNote.emit(card)

    },
    err => { console.log(err); }
  )
}

/**
 * 
 * @param card 
 */
TrashNote(card) {
  console.log(card,card.id);
  card.isTrash = true;
  this.service.UpdateNotes(card.id, card,headers_object).subscribe(
    data => {
      console.log(data);
      this.setNote.emit(card)
    },
    err => { console.log(err); }
  )
}

/**
 * 
 * @param card 
 */
Restore(card) {
  console.log(card);
  card.isTrash = false;
  this.service.UpdateNotes(card.id, card,headers_object).subscribe(
    data => {
      console.log(data);
      this.setNote.emit(card);
      
    },
    err => { console.log(err); }
  )
}
checkBox(label){
  // var UserId=localStorage.getItem('UserId');
  this.cards.label=label;
  console.log(this.cards.id)
  console.log("After Check"+this.Label.labels);
 
  this.service.UpdateNotes(this.cards.id,this.cards,headers_object).subscribe(
    (res: any) => {
    //  console.log("In Icon="+UserId,this.Label)
    },
    // err => {
    //   if (err.status == 400)
    //     this.toastr.error('Insert Correct Data');
    //   else
    //     console.log(err);
    // }
  );
  }
  Today(card)
{
  var date = new Date();
  date.setHours(20,0,0)
  card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate() + " " +  date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
  this.service.UpdateNotes(card.id,card,headers_object).subscribe(data =>{
    console.log(data);
    this.setNote.emit({});
  },err =>{
    console.log(err);
  })
}

Tomorrow(card)
  {
    var date = new Date();
    date.setHours(8,0,0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (date.getDate()+1) + " " +  date.getHours() + ":" + date.getMinutes();
    this.service.UpdateNotes(card.id,card,headers_object).subscribe(data =>{
      console.log(data);
      this.setNote.emit({});
    },err =>{
      console.log(err);
    })
  }

  nextWeek(card)
  {
    var date = new Date();
    date.setHours(8,0,0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (date.getDate()+7) + " " +  date.getHours() + ":" + date.getMinutes();
    this.service.UpdateNotes(card.id,card,headers_object).subscribe(data =>{
      console.log(data);
      this.setNote.emit({});
    },err =>{
      console.log(err);
    })
}
  openDialog(cards) {
     Email:localStorage.getItem('Email');
     console.log(cards);
     
    // const dialogRef = this.dialog.open(EditNotes);
    const dialogRef = this.dialog.open(CollaboratorsComponent,
      {
       data:cards
        
      });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'change') {
        console.log("take action here");
      }
      else {
        console.log("execute");
        // console.log(note);

        // console.log(note.id);
        this.service.UpdateNotes(this.Email,this.Email,headers_object).subscribe(data => {
          console.log(data);
          // this.Delete.emit({});
        }, err => {
          console.log(err);
        })
      }
    })
  }
}