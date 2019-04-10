import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit() {
  }
  onLogout(){
    localStorage.removeItem('token');
    alert("successfully logout");
    this.router.navigate(['/user/login']);
  }
}