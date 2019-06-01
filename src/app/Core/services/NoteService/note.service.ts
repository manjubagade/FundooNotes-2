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
    console.log("In Main Components"+UserId);
    return this.http.get(environment.BaseURI + '/Notes/viewNotes/' + UserId);
  }

  UpdateNotes(note, id) {
    console.log("In Service Update 55555" + note, id);
    return this.http.put(environment.BaseURI + '/Notes/updateNotes/' + id, note);
  }

  Trash(id, card) {
    return this.http.put(environment.BaseURI + '/Notes/updateNotes/' + id, card)
  }

  ArchiveNote(id, card) {
    return this.httpService.update('Notes/' + id, card)
  }

  GetArchiveNotes(UserId) {
    return this.httpService.Get('Notes/archive/' + UserId)
  }
  
  ViewInTrash(UserId) {
    return this.http.get(environment.BaseURI + '/Notes/viewNotes/' + UserId);
}

  DeleteNote(id) {
    console.log("In Service Delete" + id);
    return this.http.delete(environment.BaseURI + '/Notes/delete/' + id);
  }

  // UploadImage(formData){
  // console.log("In Service UploadImage"+formData);
  // }
  AddLabel(label) {
    console.log("In Service AddLabel fvgggfvvvvvvvvvvvvvvvvv" + label);
    return this.http.post(environment.BaseURI+ '/Label/add',label);
  }
  
UpdateLabel(label,UserId){
  console.log("In Service UpdateLabel +++++++++" + label,UserId);
  return this.http.put(environment.BaseURI+ '/Label/updateLabel/'+UserId,label);
}
  SetColor(card, id) {
    console.log("In Service" + id, card);
    return this.http.put(environment.BaseURI + '/Notes/updateNotes/' + id, card)
  }
  getLabelsById(UserId: string){
    return this.http.get(environment.BaseURI + '/Label/viewLabel/' + UserId);
  }
  deleteLabel(id){
    console.log("................"+id);
    return this.http.delete(environment.BaseURI + '/Label/delete/' + id);

  }
}