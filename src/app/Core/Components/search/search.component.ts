import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/DataService/data.service';
import { NoteService } from '../../services/NoteService/note.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  userId: string;
  noteCards=[];
  SearchCard:any;
  searchText:string=''

  constructor(public dataServices: DataService, public notesService:NoteService) { }

  ngOnInit() {
    this.userId = localStorage.getItem("UserId")
    this.dataServices.currentSearchmsg.subscribe(response => {
      console.log('message in search',typeof response);
      
      this.searchText=response;
this.getallCards();
})
this.getallCards();
  }
  
getallCards(){
  var token=localStorage.getItem('token');
  var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);

  this.notesService.getNotesById(this.userId,headers_object).subscribe(data =>{
    this.noteCards=[];
    console.log(data);
    this.SearchCard=data;
  },err=>{
    console.log(err);
    
  });
}
}