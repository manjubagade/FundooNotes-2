import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './Core/Components/user/user.component';
import { RegistrationComponent } from './Core/Components/user/registration/registration.component';
import { UserService } from './Core/services/user.service';
import { LoginComponent } from './Core/Components/user/login/login.component';
import { HomeComponent } from './Core/Components/home/home.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { ForgotPasswordComponent } from './Core/Components/user/forgot-password/forgot-password.component';
import { ModelComponent } from './Core/Models/model/model.component';
import { ResetpasswordComponent } from './Core/Components/user/resetpassword/resetpassword.component';
import{  MatSidenavModule,MatTabsModule,
  MatToolbarModule,MatIconModule,MatListModule,  MatButtonModule,MatButtonToggleModule, MatProgressSpinnerModule,
  MatTooltipModule } from '@angular/material';
  import { NgMatSearchBarModule } from 'ng-mat-search-bar';
  import {MatCardModule} from '@angular/material/card';
  import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    HomeComponent,
    ForgotPasswordComponent,
    ModelComponent,
    ResetpasswordComponent,
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    NgMatSearchBarModule,
    MatCardModule,
    MatButtonModule,
    FlexLayoutModule,
    MatProgressSpinnerModule,
    MatButtonToggleModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule,
  
  ],
  providers: [UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }