import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MediaMatcher } from '@angular/cdk/layout';
import { Services } from '@angular/core/src/view';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../../services/user.service';
import { first } from 'rxjs/internal/operators/first';
import { HttpBackend, HttpClient, HttpHeaders } from '@angular/common/http';
import { MatDialog, MatSidenav } from '@angular/material';
import { EditComponent } from '../edit/edit.component';
import { LabelComponent } from '../label/label.component';
import { NoteService } from '../../services/NoteService/note.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs'
import { DataService } from '../../services/DataService/data.service';
import * as jwt_decode from 'jwt-decode';
import { environment } from 'src/environments/environment';

var token = localStorage.getItem('token');
var headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']

})
export class HomeComponent implements OnInit {
  view: boolean = true;
  search: any;
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  spinner: any;

  Header = 'FundooNotes';
  user = localStorage.getItem('user');
  profilePic;


  constructor(public dataService: DataService, private http: HttpClient, private router: Router, spinner: NgxSpinnerService, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, public userService: UserService, public service: NoteService, public dialog: MatDialog, private toastr: ToastrService) {

    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

  }

  @Input() cards;
  UserId = localStorage.getItem('token');
  Email = localStorage.getItem('Email')
  Label;
  result;
  message: boolean;
  userId;
  value: any;
  data: {
    result,
    id

  }
  ngOnInit() {
    var token = localStorage.getItem('token');
    var jwt_token = jwt_decode(token);
    var UserId = jwt_token.UserID;
    localStorage.setItem('UserId', UserId);
    this.dataService.currentMessage.subscribe(message => this.message = message);
    this.userService.getUserProfile(UserId, headers_object).subscribe(data => {
      var user = data['result'][0].fullName;
      var Email = data['result'][0].email;
      localStorage.setItem('user', user);
      localStorage.setItem('Email', data['result'][0].email);
      localStorage.setItem('profilePic', data['result'][0].image);
      this.profilePic = localStorage.getItem('profilePic');
      localStorage.removeItem(user);
      localStorage.removeItem(Email);
      localStorage.removeItem(user);
    }, err => {
      console.log(err);
    });
     


    this.Label = this.Label;

    var ProfileUrl = localStorage.getItem("profilePic");
    var user = localStorage.getItem('user');
this.service.pushNotification(UserId,headers_object).subscribe(data=>{
  console.log(data);
  

})
    this.service.getLabelsById(UserId, headers_object).subscribe(
      data => {
        this.Label = data;
        var Abc = this.Label;

      }

    ), (err: any) => {
      console.log(err);
    };
  }

  EditLabel(labelid){
    this.dataService.changeSearchMsg(labelid);
    this.router.navigateByUrl('home/displaylabels');
  }

  onLogout() {
    localStorage.removeItem('token');
    localStorage.removeItem('UserId');
    localStorage.removeItem('profilePic');
    localStorage.removeItem('user');
    localStorage.removeItem('Email');
    alert("successfully logout");
    this.router.navigate(['/user/login']);
  }

  refresh() {
    window.location.reload();
    this.spinner.show();
  }

  changeView() {
    this.view = !this.view;
    this.dataService.changeMessage(this.view);
  }
  lookfor() {
    this.dataService.changeSearchMsg(this.value)
  }
  /**
   * 
   */
  goSearch() {
    this.router.navigate(['./home/search'])
  }

  openDialog() {
    const dialogRef = this.dialog.open(LabelComponent, {
      data: this.Label

    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(">>>>>>>>>>>>> " + result)
      var Abc = {
        labels: result,
        UserId: localStorage.getItem('UserId')
      }

      if (Abc.labels != null) {
        console.log("<<<<<<<<< Changed" + Abc)
        console.log("In Home Component", result);
        console.log("In Home Component", FormData);
        console.log("take action here");
        this.service.AddLabel(Abc).subscribe(data => {

        })
      }

      else {
        var UserId = localStorage.getItem('UserId');
        console.log("Result=" + Abc, UserId);
        console.log("in Home Label execute", Abc);

      }


    })


  }
  public uploadFile = (files) => {
    var UserId = localStorage.getItem('UserId');
    if (files.length === 0) {
      console.log(files);
      return
    }

    console.log("event", event);

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    var t = localStorage.getItem('token');
    var headers_object = new HttpHeaders().set("Authorization", "Bearer " + t);
    this.userService.AddProfile(UserId, formData, t).subscribe(data => {
      console.log("Image ew=" + data['result']);
      console.log("Image Im=" + data['Image']);
    })
  }
}