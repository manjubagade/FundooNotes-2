import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MediaMatcher } from '@angular/cdk/layout';
import { Services } from '@angular/core/src/view';
import { UserService } from '../../services/user.service';
import { first } from 'rxjs/internal/operators/first';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [ './home.component.css']
})
export class HomeComponent implements OnInit {
  users: UserService[] = [];
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  
  constructor(private router:Router,changeDetectorRef: ChangeDetectorRef,media:MediaMatcher) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }
  
  ngOnInit() {
   
    }
  onLogout(){
    localStorage.removeItem('token');
    alert("successfully logout");
    this.router.navigate(['/user/login']);
  }
  
  shouldRun = [/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));

}