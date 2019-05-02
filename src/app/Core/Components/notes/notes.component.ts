import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import * as jwt_decode from "jwt-decode";

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
   

  constructor(private router:Router,public service:UserService,private toastr: ToastrService) { }

  ngOnInit() {
  }
  
  AddNotes() {
    var token=localStorage.getItem('token');
  var jwt_token=jwt_decode(token);
  console.log(jwt_token.UserID);
  //localStorage.setItem("UserID",jwt_token.UserID)
  //this.token_id=localStorage.getItem("UserID")

    this.service.AddNotes(this.form.value).subscribe(
      (res: any) => {
        console.log(this.form.value);
        this.router.navigateByUrl('/home');
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Insert Correct Data');
        else
          console.log(err);
      }
    );
  }
}