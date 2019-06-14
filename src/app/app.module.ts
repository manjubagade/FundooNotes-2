import { BrowserModule } from '@angular/platform-browser';
import { NgModule,Component,ElementRef } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormControl } from "@angular/forms";
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
  MatToolbarModule,MatIconModule,MatListModule, MatButtonModule,MatButtonToggleModule, MatProgressSpinnerModule,
  MatTooltipModule, MatInputModule,
  MatExpansionPanel,MatExpansionModule, MatOption, MatFormFieldModule, MatMenuModule, MatCheckboxModule, MatSelectModule,MatChipInputEvent, MatChipsModule} from '@angular/material';
  import {MatDialogModule} from '@angular/material/dialog';
  import { NgMatSearchBarModule } from 'ng-mat-search-bar';
  import {MatCardModule} from '@angular/material/card';
  import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
  import { FlexLayoutModule } from '@angular/flex-layout';
  import {MediaMatcher} from '@angular/cdk/layout';
  import { NgxSpinnerModule } from 'ngx-spinner';
import { NotesComponent } from './Core/Components/notes/notes.component';
import { RemindersComponent } from './Core/Components/reminders/reminders.component';
import { ArchiveComponent } from './Core/Components/archive/archive.component';
import { TrashComponent } from './Core/Components/trash/trash.component';
import { MainnoteComponent } from './Core/Components/mainnote/mainnote.component';
import { DisplayNotesComponent } from './Core/Components/display-notes/display-notes.component';
import { IconComponent } from './Core/Components/icon/icon.component';
import { EditComponent } from './Core/Components/edit/edit.component';
import { LabelComponent } from './Core/Components/label/label.component';
import { PipePipe } from './Core/Pipe/pipe.pipe';
import { CollaboratorsComponent } from './Core/Components/Collaborators/collaborators.component';
import { SearchComponent } from './Core/Components/search/search.component';
import {
  SocialLoginModule,
  AuthServiceConfig,
  GoogleLoginProvider,
  FacebookLoginProvider,
} from 'angular-6-social-login';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { InputTrimModule } from 'ng2-trim-directive';

let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("Google-OAuth-Client-Id")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("558981037962992")
  }
]);
 
export function provideConfig() {
  return config;
}

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
    NotesComponent,
    RemindersComponent,
    ArchiveComponent,
    TrashComponent,
    MainnoteComponent,
    DisplayNotesComponent,
    IconComponent,
    EditComponent,
    LabelComponent,
    PipePipe,
    CollaboratorsComponent,
    SearchComponent,
 
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    SocialLoginModule,
    MatSelectModule,
    MatTabsModule,
    DragDropModule,
    MatInputModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatDialogModule,
    MatTooltipModule,
    NgMatSearchBarModule,
    MatCardModule,
    InputTrimModule,
    MatButtonModule,
    MatExpansionModule,
    FlexLayoutModule,
    MatMenuModule,
    MatChipsModule,
    
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
  },
  {
    provide: AuthServiceConfig,
    useFactory: provideConfig
  }
],
  bootstrap: [AppComponent]
})
export class AppModule { }