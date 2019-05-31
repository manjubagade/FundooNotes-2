import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MediaMatcher } from '@angular/cdk/layout';
import { Services } from '@angular/core/src/view';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../../services/user.service';
import { first } from 'rxjs/internal/operators/first';
import { HttpBackend, HttpClient } from '@angular/common/http';
import { MatDialog, MatSidenav } from '@angular/material';
import { EditComponent } from '../edit/edit.component';
import { LabelComponent } from '../label/label.component';
import { NoteService } from '../../services/NoteService/note.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs'
import { DataService } from '../../services/DataService/data.service';
import * as jwt_decode from 'jwt-decode';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']

})
export class HomeComponent implements OnInit {
  view: boolean = true;

  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  spinner: any;

  Header = 'FundooNotes';

  constructor(public dataService: DataService, private http: HttpClient, private router: Router, spinner: NgxSpinnerService, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, public userService: UserService, public service: NoteService, public dialog: MatDialog, private toastr: ToastrService) {

    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

  }

  @Input() cards;
  UserId = localStorage.getItem('token');
  Label;
  result;
  userId;
  data: {
    result,
    id
  }
  ngOnInit() {
    var token = localStorage.getItem('token');
    var jwt_token = jwt_decode(token);
    var UserId= jwt_token.UserID;
    localStorage.setItem('UserId',UserId);
    console.log("In Home UserId"+UserId)
    this.Label = this.Label;

    
    var Profilepic = localStorage.getItem("profilePic");
    console.log("9999999900000000" + Profilepic);
   console.log(UserId);

    this.userService.getUserProfile(UserId).subscribe(data => {
      var Profilepic = data['result'];
      localStorage.setItem('profilePic', Profilepic);

    }, err => {
      console.log(err);
    });


    this.service.getLabelsById(UserId).subscribe(
      data => {
        this.Label = data;
        var Abc = this.Label;
        console.log(Abc);
      }

    ), (err: any) => {
      console.log(err);
    };
  }

  onLogout() {
    localStorage.removeItem('token');
    localStorage.removeItem('UserID');
    localStorage.removeItem('profilePic');
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

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    // var id=this.cards.id;
    this.http.post(environment.BaseURI + '/User/profilepic/' + UserId, formData, { reportProgress: true, observe: 'events' })
      .subscribe(data => {

        console.log("Image ew=" + data['result']);
        console.log("Image Im=" + data['Image']);

      });
  }
}