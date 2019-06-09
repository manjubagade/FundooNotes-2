import { Component, OnInit, Input } from '@angular/core';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpHeaders } from '@angular/common/http';
import { MatCardSmImage } from '@angular/material';

var headers_object = new HttpHeaders();
    headers_object.append('Content-Type', 'application/json');
    headers_object.append("Authorization", "Bearer " + localStorage.getItem('token'));
@Component({
  selector: 'app-reminders',
  templateUrl: './reminders.component.html',
  styleUrls: ['./reminders.component.css']
})
export class RemindersComponent implements OnInit {

  constructor(private route:Router,private service:NoteService) { }
  @Input() cards;
  notes;
  ngOnInit() {
    
    this.reminder();

  }
  reminder(){
    var UserId=localStorage.getItem('UserId');
    this.service.reminders(UserId,headers_object).subscribe(data =>{
      console.log(data);
      
      this.notes=data['result'];
      console.log(this.notes);
      
      console.log(this.notes)
    },err =>{
      console.log(err);
    }
      )
  }
  

}
