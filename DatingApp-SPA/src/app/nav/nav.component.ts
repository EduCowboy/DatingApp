import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../_services/authService.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthServiceService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    }, error => {
      /* catchError(e => throwError(this.errorHandler(e)));
      console.log('Failed!'); */
      this.alertify.error(error);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
    /*const token = localStorage.getItem('token');
    return !!token; */
  }

  logOut() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out');
  }

  errorHandler(erro) {
    console.log(erro);
  }
}
