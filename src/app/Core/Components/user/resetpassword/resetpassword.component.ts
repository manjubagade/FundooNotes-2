import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/Core/services/user.service';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent implements OnInit {
  formModel = {
    Email : this.route.snapshot.paramMap.get("Email")
  }

  constructor(public service: UserService, private toastr: ToastrService,private router:Router,private route: ActivatedRoute) { 
   this.route.params.subscribe(params=>console.log(params));
  }

  ngOnInit() {
    this.service.formModel.reset();
    
  }
  onSubmit(form: NgForm) {
    this.service.Resetpassword(form.value).subscribe(
      (res: any) => {
        if (res.succeeded) {
         this.service.formModel.reset();
         this.toastr.success('Reset');
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