import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm, Form } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class NoteService {
  [x: string]: any;

  /**
   *
   */
  constructor(private http: HttpClient) { }

  AddNotes(formData, UserId) {
    formData.UserId = UserId;
    return this.http.post(environment.BaseURI + '/Notes/addNotes', formData, UserId);
  }

  getNotesById(UserId: string) {
    return this.http.get(environment.BaseURI + '/Notes/viewNotes/' + UserId);
  }

  UpdateNotes(note, id) {
    console.log("In Service Update" + note, id);
    return this.http.put(environment.BaseURI + '/Notes/updateNotes/' + id, note);
  }

  DeleteNote(id) {
    console.log("In Service Delete" + id);
    return this.http.delete(environment.BaseURI + '/Notes/delete/' + id);
  }

  UploadImage(formData){
  console.log("In Service UploadImage"+formData);
  }

AddLabel(Label,UserId){
return this.http.post(environment.BaseURI+ '/Label/add/', Label,UserId);
}

SetColor(card,id){
  
   console.log("In Service"+id,card);
   return this.http.put(environment.BaseURI+ '/Notes/updateNotes/'+id, card)
}

}