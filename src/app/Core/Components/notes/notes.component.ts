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
import { refreshDescendantViews } from '@angular/core/src/render3/instructions';
import { HomeComponent } from '../home/home.component';
import { HttpHeaders } from '@angular/common/http';
import { empty } from 'rxjs';
import { InputTrimModule } from 'ng2-trim-directive';
@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  form = new FormGroup({
    title: new FormControl(),
    Description: new FormControl(),
    
    
  });
  constructor(private router: Router, public service: NoteService, private toastr: ToastrService, private home: HomeComponent) {
  }
  color='#ffffff';
  image;
  @Input() cards;
  @Output() public notes = new EventEmitter();
  ngOnInit() {
    var token = localStorage.getItem('token');

    var headers_object = new HttpHeaders();
    headers_object.append('Content-Type', 'application/json');
    headers_object.append("Authorization", "Bearer " + token);

    const httpOptions = {
      headers: headers_object
    };
  }

  AddNotes() {
    try {
      var token = localStorage.getItem('token');

      var jwt_token = jwt_decode(token);
      console.log(jwt_token.UserID);
      localStorage.setItem("UserId", jwt_token.UserID);
      var UserId = localStorage.getItem("UserId");
     

      var token=localStorage.getItem('token');
      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);
     this.form.value.title!=null&&this.form.value.Description!=null&&this.form.value.title.trim()!=''
      if ((this.form.value.title!==null||this.form.value.title.trim()!==''||this.form.value.title.trim!==''||this.form.value.title!=='')||(this.form.value.title.trim()!=='' || this.form.value.title!==''|| this.form.value.title!== 'undefined'|| this.form.value.Description.trim()!=='')||(this.form.value.title.length>0 ||this.form.value.Description.length>0)) {
       console.log('yhfsayu');
       
        
        this.service.AddNotes(this.form.value, UserId,headers_object).subscribe(
          (res: any) => {
           this.form.reset();
            //  this.router.navigateByUrl('/home');
            this.notes.emit(res);
            
          },
          err => {
            if (err.status == 400)
              this.toastr.error('Insert Correct Data');
            else
              console.log(err);
          }
        );
      }
      else {
        // this.home.refresh();
      }
    }
  
    catch (error) {
      console.log('invalid token format', error);
    }
  }
}
