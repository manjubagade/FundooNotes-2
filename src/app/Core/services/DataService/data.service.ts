import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm, Form } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DataService {
  private searchMessage = new BehaviorSubject<string>('');
  currentSearchmsg = this.searchMessage.asObservable();

  private messageSource = new BehaviorSubject(true);
  currentMessage = this.messageSource.asObservable();

  constructor() { }
   
  changeMessage(current:boolean){
  this.messageSource.next(current);
  }
  changeSearchMsg(view: string){
    console.log("in dataService search",view);
this.searchMessage.next(view);

  }
}