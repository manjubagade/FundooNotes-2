import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
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


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [ './home.component.css']
})
export class HomeComponent implements OnInit {
  
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  spinner: any;
  Header='FundooNotes';
  constructor(private router:Router,spinner: NgxSpinnerService,changeDetectorRef: ChangeDetectorRef,media:MediaMatcher,userService:UserService,public dialog: MatDialog) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }
  
  ngOnInit() {
    }
  onLogout(){
    localStorage.removeItem('token');
    localStorage.removeItem('UserID');
    alert("successfully logout");
    this.router.navigate(['/user/login']);
  }
  refresh() {
    window.location.reload();
    this.spinner.show();
}

openDialog(Label){
  console.log(Label);
  const dialogRef=this.dialog.open(LabelComponent,{
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
   if(result==='change'){
     console.log("In Home Component");
    console.log("take action here");
  }
   else{
   
 console.log("execute");
 }
 })
}
}