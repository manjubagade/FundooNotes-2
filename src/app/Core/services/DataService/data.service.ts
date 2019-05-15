import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm, Form } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DataService {

  private messageSource = new BehaviorSubject(true);
  currentMessage = this.messageSource.asObservable();

  private msg=new BehaviorSubject({type:''});
  current=this.msg.asObservable();

  constructor() { }
  
  changeMessage(current:boolean){
  this.messageSource.next(current);
  }
  change(msg:any){
    this.msg.next(msg);
  }
  }