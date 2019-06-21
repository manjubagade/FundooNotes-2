import { Component, OnInit } from '@angular/core';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpHeaders } from '@angular/common/http';
import { DataService } from '../../services/DataService/data.service';

var token = localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);
@Component({
  selector: 'app-display-labels',
  templateUrl: './display-labels.component.html',
  styleUrls: ['./display-labels.component.css']
})
export class DisplayLabelsComponent implements OnInit {
  labelid:any;
  NotesData:any;
  constructor(private service:NoteService,private dataservice:DataService) { }

  ngOnInit() {
    var Userid=localStorage.getItem('UserId');
    var token = localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);
    this.dataservice.currentSearchmsg.subscribe(data=>{
      this.labelid=data;
    console.log(this.labelid);
    this.NotesData={
      UserId:Userid,
      labelid:this.labelid
    }
    
    })
//     this.service.LabelNotes(Userid,this.NotesData,headers_object).subscribe(data=>{
// console.log(data);

//     })
  }

 
}