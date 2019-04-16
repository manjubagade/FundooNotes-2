import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/Core/services/user.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent implements OnInit {
  

  constructor(public service: UserService, private toastr: ToastrService,private router:Router) { 
   
  }

  ngOnInit() {
    this.service.formModel.reset();
  }
  onSubmit(form: NgForm) {
    this.service.Resetpassword(form.value).subscribe(
      (res: any) => {
        if (res.succeeded) {
         this.service.formModel.reset();
    }
},
err => {
  if (err.status == 400)
    this.toastr.error('User does Not Exist');
  else
    console.log(err);
}
    )}
  }