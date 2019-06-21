import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpHeaders } from '@angular/common/http';

var token = localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);
@Component({
  selector: 'app-collaborators',
  templateUrl: './collaborators.component.html',
  styleUrls: ['./collaborators.component.css']
})
export class CollaboratorsComponent implements OnInit {
  AddPerson;
  Collaborator;
  profilePic=localStorage.getItem('profilePic');
 user=localStorage.getItem('user');
 Email=localStorage.getItem('Email');
  constructor(public dialog: MatDialogRef<CollaboratorsComponent>, @Inject(MAT_DIALOG_DATA) public data,private service:NoteService) { }

  ngOnInit() {
    console.log(this.data);
    var user=localStorage.getItem('user');
    var Email=localStorage.getItem('Email');
    var UserId=localStorage.getItem('UserId');
    
    this.service.ViewCollaborators(UserId, headers_object).subscribe(data => {
        
      this.Collaborator={
        Collaborator : data,
    }
   
    console.log(this.Collaborator);
  
     if(this.Collaborator.Collaborator.receiverEmail==localStorage.getItem('Email')){
      console.log("Success");
      
    }
    
 })
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