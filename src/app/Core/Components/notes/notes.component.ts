import { Component, OnInit, Inject, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import * as jwt_decode from 'jwt-decode';
import { Token } from '@angular/compiler';
import { MAT_DIALOG_DATA } from '@angular/material';
import { NoteService } from '../../services/NoteService/note.service';
import { EventEmitter } from 'events';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  form=new FormGroup({
    title : new FormControl(),
    Description : new FormControl(),
    
   });
  constructor(private router:Router,public service:NoteService,private toastr: ToastrService) { 
  }
  color;
  @Input() cards;
  ngOnInit() {
  }
  
  AddNotes() {
    try {
      var token=localStorage.getItem('token');  
  
      var jwt_token=jwt_decode(token);
      console.log(jwt_token.UserID);
      localStorage.setItem("UserId",jwt_token.UserID);
      var UserId=localStorage.getItem("UserId");
     
    } catch (error) {
      console.log('invalid token format', error);
    }
    // console.log("ssssssssssss"+title);
    
    this.service.AddNotes(this.form.value,UserId).subscribe(
      (res: any) => {
        this.router.navigateByUrl('/home');
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Insert Correct Data');
        else
          console.log(err);
      }
    );
    
  
    var textarea = document.querySelector('textarea');

    textarea.addEventListener('keydown', autosize);
                 
    function autosize(){
      var el = this;
      setTimeout(function(){
        el.style.cssText = 'height:auto; padding:0';
        // for box-sizing other than "content-box" use:
        // el.style.cssText = '-moz-box-sizing:content-box';
        el.style.cssText = 'height:' + el.scrollHeight + 'px';
      },0);
    }
    
  }

}