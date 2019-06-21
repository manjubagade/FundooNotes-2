import { Component, OnInit, Input, Inject, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { MatDialogRef, MAT_DIALOG_DATA, mixinColor, MatDialog } from '@angular/material';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpClient, HttpEventType, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CollaboratorsComponent } from '../Collaborators/collaborators.component';
import { EditComponent } from '../edit/edit.component';

var token = localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);

@Component({
  selector: 'app-icon',
  templateUrl: './icon.component.html',
  styleUrls: ['./icon.component.css']

})
export class IconComponent implements OnInit {
  public progress: number;
  public message: string;
  @Output() public messageEvent = new EventEmitter()
  Label;
  Email;
  reminder;
  LabelOnNotes:any;
  flag = true
  @Output() public onUploadFinished = new EventEmitter();
  @Output() public setColor = new EventEmitter();

  @Input() archivedicon
  @Input() trashed
  @Output() setNote = new EventEmitter();

  trash: boolean = true;
  archive: boolean = true;
  unarchive: boolean = true;

  constructor(private route: Router, private service: NoteService, private http: HttpClient, public dialog: MatDialog) {
    this.Label = this.Label;
    
  }
  @Input() cards;

  color;

  ngOnInit() {
   

    var UserID = localStorage.getItem('UserId');

    this.service.getLabelsById(UserID, headers_object).subscribe(
      data => {
        this.Label = data;
      }

    ), (err: any) => {
      console.log(err);
    };
   
  }


  Delete(card) {

    this.service.DeleteNote(this.cards.id).subscribe(data => {
     
      this.setNote.emit(data);
    }, err => {
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
    var id = this.cards.id;
    this.service.AddImage(id, formData).subscribe(data => {
      this.setColor.emit(this.uploadFile);
     
    })


  }

  setcolor(color, cards) {
    this.cards.color = color;
 

    this.service.UpdateNotes(this.cards.id, this.cards, headers_object).subscribe(data => {
      this.setColor.emit(this.setcolor);

    }, err => {
      console.log(err);
    })

  }

  Archive(card) {
    card.IsArchive = true;
    
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(
      data => {
      
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
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(
      data => {
      
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
    
    card.isTrash = true;
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(
      data => {
      
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
   
    card.isTrash = false;
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(
      data => {
        
        this.setNote.emit(card);

      },
      err => { console.log(err); }
    )
  }
  checkBox(label,id) {
    
    // this.cards.label = label;
  console.log("Checkbox",label,this.cards.id);
  var UserId=localStorage.getItem('UserId');

var LabelOnNotes={
  labelId:label,
  NotesId:this.cards.id,
  UserId,
}

console.log(LabelOnNotes,"sdfghjk");


    this.service.AddNotesLabel(LabelOnNotes, headers_object).subscribe(
      (res:any) => {
        this.setNote.emit(LabelOnNotes);
      },
    
    )
  }
  
  Today(card) {
    var date = new Date();
    date.setHours(20, 0, 0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(data => {
    
      // this.setNote.emit({});
    }, err => {
      console.log(err);
    })
  }

  Tomorrow(card) {
    var date = new Date();
    date.setHours(8, 0, 0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (date.getDate() + 1) + " " + date.getHours() + ":" + date.getMinutes();
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(data => {
      
      // this.setNote.emit({});
    }, err => {
      console.log(err);
    })
  }

  nextWeek(card) {
    var date = new Date();
    date.setHours(8, 0, 0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (date.getDate() + 7) + " " + date.getHours() + ":" + date.getMinutes();
    this.service.UpdateNotes(card.id, card, headers_object).subscribe(data => {
      
      // this.setNote.emit({});
    }, err => {
      console.log(err);
    })
  }
  openDialog(cards) {
    Email: localStorage.getItem('Email');
  

    // const dialogRef = this.dialog.open(EditNotes);
    const dialogRef = this.dialog.open(CollaboratorsComponent,
      {
        data: cards

      });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'change') {
    
      }
      else {
   
        this.service.UpdateNotes(this.Email, this.Email, headers_object).subscribe(data => {
         
        }, err => {
          console.log(err);
        })
      }
    })
  }
}