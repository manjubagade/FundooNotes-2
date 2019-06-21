import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm, Form } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';

var token = localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);


@Injectable({
  providedIn: 'root'
})
export class NoteService {

  [x: string]: any;



  constructor(private http: HttpClient) {

  }

  AddNotes(formData, UserId, token) {
    formData.UserId = UserId;
    return this.http.post(environment.BaseURI + '/Notes/addNotes', formData, token);
  }

  getNotesById(UserId: string, token) {

    return this.http.get(environment.BaseURI + '/Notes/view/' + UserId, token);
  }

  UpdateNotes(id, note, headers_object) {
    console.log("In Service Update 55555 " + id, note);
    return this.http.put(environment.BaseURI + '/Notes/updateNotes/' + id, note, headers_object);
  }
  pushNotification(UserId,headers_object){
    return this.http.get(environment.BaseURI + '/Notes/reminder/' + UserId,headers_object);
  }
  GetArchiveNotes(UserId) {
    return this.http.get(environment.BaseURI + '/Notes/archive/' + UserId)
  }

  ViewInTrash(UserId, headers_object) {
    return this.http.get(environment.BaseURI + '/Notes/trash/' + UserId, headers_object);
  }

  DeleteNote(id) {
    console.log("In Service Delete" + id);
    return this.http.delete(environment.BaseURI + '/Notes/delete/' + id);
  }

  AddImage(id, formData) {
    return this.http.post(environment.BaseURI + '/Notes/image/' + id, formData);
  }

  reminders(UserId, headers_object) {
    return this.http.get(environment.BaseURI + '/Notes/reminder/' + UserId, headers_object);
  }
  AddLabel(label) {
    console.log("In Service AddLabel fvgggfvvvvvvvvvvvvvvvvv" + label);
    return this.http.post(environment.BaseURI + '/Label/add', label);
  }

  UpdateLabel(label, UserId, headers_object) {
    console.log("In Service UpdateLabel +++++++++" + label, UserId);
    return this.http.put(environment.BaseURI + '/Label/updateLabel/' + UserId, label, headers_object);
  }
  // Notes Label Handling
  viewNotesLabel(UserId, headers_object) {
    return this.http.get(environment.BaseURI + '/Label/viewalllabel/' + UserId, headers_object)

  }

  DeleteNotesLabel(id) {

    return this.http.delete(environment.BaseURI + '/Label/deletenoteslabel/' + id, this.headers_object)
  }

  AddNotesLabel(data, headers_object) {
    console.log(data);

    return this.http.post(environment.BaseURI + '/Label/addlabel', data, headers_object)

  }
  getLabelsById(UserId: string, headers_object) {
    return this.http.get(environment.BaseURI + '/Label/viewLabel/' + UserId, headers_object);
  }
  displayLabels(){
    
  }
  // 
  deleteLabel(id, headers_object) {
    console.log("................" + id);
    return this.http.delete(environment.BaseURI + '/Label/delete/' + id, headers_object);

  }
  LabelNotes(id,data){
    var  headers_object = {
      headers: new HttpHeaders({
        'Authorization' : 'bearer'+ localStorage.getItem('token')
      })
    };
    console.log("Labesssssssssssssssssss",id,data);
    
    return this.http.get(environment.BaseURI + '/Label/viewlabelnotes/' + id, data);
  }
  AddCollaborator(data) {
    console.log(data);

    return this.http.post(environment.BaseURI + '/Collaborators/addCollaborators', data);

  }
    ViewCollaborators(UserId,headers_object) {
      return this.http.get(environment.BaseURI + '/Collaborators/viewcollaborators/' + UserId,headers_object);
  }

 
  // Pushnotification(){
  //  var url= 'https://fcm.googleapis.com/fcm/send'
  //  var data={
  //   "notification": {
  //     "title": "Hello Xyz", 
  //     "body": "This is Message from Aniket"
  //    },
  //    "to" : "f0fdV_nRckA:APA91bFBi9_qwDoB02td_2TDubdbTGQT_8q7qlsKww1ExmTvh4vutd2Vpyh8n3p5OjU82wfgNmS0JX0ZQYZ9aUgIk66QjoWs8VI7RP7UoW81VQxWiMfCMh-TLQcJqiWqLlrqhDADw0KV"
     
  //  }
  //    return this.http.post(url,data,this.Authorize);
  // }

  // removeCollaborator(id){
  //   return this.http.delete(this.BaseURI+'notes/collaborator/'+id);
  //  }
  //  getCollaboratorNote(receiverEmail){
  //    return this.http.get(this.BaseURI+'notes/collaborator/'+receiverEmail);
  //  }
}