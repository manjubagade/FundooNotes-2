import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../../Core/services/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { empty } from 'rxjs';
import * as jwt_decode from 'jwt-decode';
import {
  AuthService,
  FacebookLoginProvider,
  GoogleLoginProvider
} from 'angular-6-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  FbStatus;
  formModel = {
    Email: '',
    Password: ''
   

  }
  constructor(private service: UserService, private router: Router, private toastr: ToastrService,private socialAuthService: AuthService) { }
  
  ngOnInit() {

    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    form.value.FbStatus='false';
    this.FbStatus='false';
    this.service.login(form.value,this.FbStatus).subscribe(
      (res: any) => {

        localStorage.setItem('token', res);
        
        //console.log(res);
        this.router.navigateByUrl('/home');
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Incorrect Email or password.', 'Authentication failed.');
        else if (err.ok == false) {
          this.toastr.error('Incorrect Email or password.', 'Authentication failed.');
        }
        else
          console.log(err);
      }
    );
  }

  public socialSignIn(socialPlatform : string) {
    let socialPlatformProvider;
    if(socialPlatform == "facebook"){
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    }else if(socialPlatform == "google"){
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    }
    this.socialAuthService.signIn(socialPlatformProvider).then(
        (userData) => {
          console.log(socialPlatform+" sign in data : " , userData);
         var form={
          Email:userData['email'],
          Password:'Aniket',
          FbStatus:'true'
          }
          this.FbStatus='true';
          console.log(form);
          
          this.service.login(form,this.FbStatus).subscribe(
            (res: any) => {
      
              localStorage.setItem('token', res);
              
              console.log(res);
              this.router.navigateByUrl('/home');
            },
          
          )},
        err => {
          if (err.status == 400)
            this.toastr.error('Incorrect Email or password.', 'Authentication failed.');
          else if (err.ok == false) {
            this.toastr.error('Incorrect Email or password.', 'Authentication failed.');
          }
          else
            console.log(err);
        }
      );
      
  }
}