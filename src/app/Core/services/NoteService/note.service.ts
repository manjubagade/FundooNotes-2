import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm, Form } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';

var t=localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + t);

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  
  [x: string]: any;

 

  constructor(private http: HttpClient) { 
   
  }

  AddNotes(formData, UserId,t) {
    formData.UserId = UserId;
    return this.http.post(environment.BaseURI + '/Notes/addNotes', formData,t);
  }

  getNotesById(UserId: string,t) {
    console.log("In Main Components"+UserId);
    return this.http.get(environment.BaseURI + '/Notes/view/' + UserId,t);
  }

  UpdateNotes(id, note,headers_object) {
    console.log("In Service Update 55555 " + id, note);
    return this.http.put(environment.BaseURI + '/Notes/updateNotes/' + id, note,headers_object);
  }

  GetArchiveNotes(UserId) {
    return this.http.get(environment.BaseURI +'/Notes/archive/' + UserId)
  }
  
  ViewInTrash(UserId,headers_object) {
    return this.http.get(environment.BaseURI + '/Notes/trash/' + UserId,headers_object);
}

  DeleteNote(id) {
    console.log("In Service Delete" + id);
    return this.http.delete(environment.BaseURI + '/Notes/delete/' + id);
  }

  AddImage(id, formData){
  return this.http.post(environment.BaseURI+'/Notes/image/'+id, formData);
}

reminders(UserId,headers_object)
  {
    return this.http.get(environment.BaseURI+'/Notes/reminder/'+UserId,headers_object);
}
  AddLabel(label) {
    console.log("In Service AddLabel fvgggfvvvvvvvvvvvvvvvvv" + label);
    return this.http.post(environment.BaseURI+ '/Label/add',label);
  }
  
UpdateLabel(label,UserId,headers_object){
  console.log("In Service UpdateLabel +++++++++" + label,UserId);
  return this.http.put(environment.BaseURI+ '/Label/updateLabel/'+UserId,label,headers_object);
}
 
  getLabelsById(UserId: string,headers_object){
    return this.http.get(environment.BaseURI + '/Label/viewLabel/' + UserId,headers_object);
  }
  deleteLabel(id,headers_object){
    console.log("................"+id);
    return this.http.delete(environment.BaseURI + '/Label/delete/' + id,headers_object);

  }
  AddCollaborator(data){
    console.log(data);
    
    return this.http.post(environment.BaseURI+ '/Collaborators/addCollaborators',data);

  }
  ViewCollaborators(UserId,headers_object) {
    return this.http.get(environment.BaseURI + '/Collaborators/viewcollaborators/' + UserId,headers_object);
}
}