import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../_services/authService.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  
  constructor(private authService: AuthServiceService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Success!');
    }, error => {
      catchError(e => throwError(this.errorHandler(e)));
      console.log('Failed!');
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logOut() {
    localStorage.removeItem('token');
    console.log('logged out');
  }

  errorHandler(erro){
    console.log(erro)
  }
}
