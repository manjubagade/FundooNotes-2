import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm, Form } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { single } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  [x: string]: any;

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    FullName: [''],
    Passwords: this.fb.group({
      
      Password: ['', [Validators.required, Validators.minLength(3)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(environment.BaseURI + '/User/register', body);
  }

  ForgotPassword(formData) {
    return this.http.post(environment.BaseURI + '/User/forgotPassword', formData);
  }

  Resetpassword(formData) {
    return this.http.post(environment.BaseURI + '/User/resetPassword', formData);
  }

  login(formData) {
    return this.http.post(environment.BaseURI + '/User/login', formData);
  }

  AddProfile(){
    return this.http.post(environment.BaseURI + '/User/profilepic','');
  }
  
  getUserProfile(id) {
    
    return this.http.get(environment.BaseURI+'/User/getprofilepic/'+id);
   // var tokenHeader=new HttpHeaders({'Authorization':'Bearer '+localStorage.getItem('token')})
  //  return this.http.get(environment.BaseURI + '/UserProfile',{headers:tokenHeader});
  }

}