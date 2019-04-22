import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
 
  private _mobileQueryListener: () => void;
  fillerNav = Array(50).fill(0).map((_, i) => 'Nav Item ${i + 1}');
  constructor(private router:Router) { }
  
  ngOnInit() {
  }
  onLogout(){
    localStorage.removeItem('token');
    alert("successfully logout");
    this.router.navigate(['/user/login']);
  }
  
}