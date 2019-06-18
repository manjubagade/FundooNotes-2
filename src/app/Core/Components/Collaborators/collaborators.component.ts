import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material';
import { NoteService } from '../../services/NoteService/note.service';

@Component({
  selector: 'app-collaborators',
  templateUrl: './collaborators.component.html',
  styleUrls: ['./collaborators.component.css']
})
export class CollaboratorsComponent implements OnInit {
  AddPerson;
  profilePic=localStorage.getItem('profilePic');
 user=localStorage.getItem('user');
 Email=localStorage.getItem('Email');
  constructor(public dialog: MatDialogRef<CollaboratorsComponent>, @Inject(MAT_DIALOG_DATA) public data,private service:NoteService) { }

  ngOnInit() {
    console.log(this.data);
    var user=localStorage.getItem('user');
    var Email=localStorage.getItem('Email');
    
  }

  Cancle(){
    this.dialog.close();
  }

 Update(AddPerson){
   
    var id=this.data.id;
    var Email=localStorage.getItem('Email');
    var SenderEmail=Email;
    var ReceiverEmail=AddPerson;
    var UserId=this.data.userId;
    var Email=localStorage.getItem('Email');
   var details={
    SenderEmail,
    id,
    ReceiverEmail,
    UserId
   }
   this.service.AddCollaborator(details).subscribe(data=>{
    console.log(data);
    
   })

 }
}