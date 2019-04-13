import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/Core/services/user.service';

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
  onSubmit() {
    this.service.Resetpassword().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.service.formModel.reset();
        }
    },
  
    )}
  }