import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { Console } from '@angular/core/src/console';
import { UserComponent } from '../user/user.component';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { EditComponent } from '../edit/edit.component';
import { IconComponent } from '../icon/icon.component';
import { NoteService } from '../../services/NoteService/note.service';


@Component({
  selector: 'app-display-notes',
  templateUrl: './display-notes.component.html',
  styleUrls: ['./display-notes.component.css']
})
export class DisplayNotesComponent implements OnInit {
   

  constructor(private service:NoteService, route:Router,private toastr:ToastrService,public dialog: MatDialog) { 
  }
  @Input() cards;
  ngOnInit() {
  }
 
  openDialog(note){
    console.log(note);
    // const dialogRef = this.dialog.open(EditNotes);
    const dialogRef = this.dialog.open(EditComponent,
      {
      data:note,
      //  height:auto,
      //  width:'auto
  });
  dialogRef.afterClosed().subscribe(result => {
    if(result==='change'){
      console.log("take action here");
    }
    else{
      
  console.log("execute");
  console.log(note);
  console.log(note.id);
 this.service.UpdateNotes(note,note.id).subscribe(data =>{
  console.log(data);
  // this.Delete.emit({});
  },err =>{
  console.log(err);
  })
  }
})
  }
}