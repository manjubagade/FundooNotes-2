import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm, Form } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Subject } from 'rxjs';



@Injectable({
  providedIn: 'root'
})

export class DataService {

  private view = new Subject();
  currentMessage = this.view.asObservable();

  constructor() { }
  subject = new Subject();

  getView() {
    this.gridview();
   

    return this.subject.asObservable();
  }
  gridview() {
    // if (this.result) {
    //   this.subject.next({ data: "row" });
    //   this.result = false;
    // }
    // else {
    //   this.subject.next({ data: "column" });
    //   this.result = true;
    // }
  }
}