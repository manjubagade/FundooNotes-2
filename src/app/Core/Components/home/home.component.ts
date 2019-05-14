import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MediaMatcher } from '@angular/cdk/layout';
import { Services } from '@angular/core/src/view';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../../services/user.service';
import { first } from 'rxjs/internal/operators/first';
import { HttpBackend } from '@angular/common/http';
import { MatDialog } from '@angular/material';
import { EditComponent } from '../edit/edit.component';
import { LabelComponent } from '../label/label.component';
import { NoteService } from '../../services/NoteService/note.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs'
import { DataService } from '../../services/DataService/data.service';

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

  constructor(public dataService: DataService, private router: Router, spinner: NgxSpinnerService, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, public userService: UserService, public service: NoteService, public dialog: MatDialog, private toastr: ToastrService) {

    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  @Input() cards;
  UserId = localStorage.getItem('token');
  Label;
  userId;
  ngOnInit() {
    this.Label = this.Label;
    var UserID = localStorage.getItem("UserId");
    this.service.getLabelsById(UserID).subscribe(
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
    alert("successfully logout");
    this.router.navigate(['/user/login']);
  }

  refresh() {
    window.location.reload();
    this.spinner.show();
  }

  changeView() {
    this.view = !this.view;
    this.dataService.gridview();
  }

  openDialog() {
    const dialogRef = this.dialog.open(LabelComponent, {
      data: this.Label
      // var UserId= localStorage.getItem('UserId');
      // this.service.AddLabel(this.Label,UserId).subscribe(data =>{
      //   console.log("In AddLabel"+UserId);
      //   console.log(data);
      //   // this.Delete.emit({});
      //   },err =>{
      //   console.log(err);
      //   })

    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(">>>>>>>>>>>>> "+result)
      var Abc = {
        labels: result,
        UserId: localStorage.getItem('UserId')
      }

        if (Abc.labels != null) {
          console.log("<<<<<<<<< Chasnged"+Abc)
          console.log("In Home Component", result);
          console.log("In Home Component", FormData);
          console.log("take action here");
          this.service.AddLabel(Abc).subscribe(data=>{

          })
        }
        else {
          var UserId = localStorage.getItem('UserId');
          console.log("Result=" + Abc, UserId);
          console.log("in Home Label execute", Abc);
         
        }
      
    })

  }
}