import { AuthGuard } from './auth/auth.guard';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './Core/Components/user/user.component';
import { RegistrationComponent } from './Core/Components/user/registration/registration.component';
import { LoginComponent } from './Core/Components/user/login/login.component';
import { HomeComponent } from './Core/Components/home/home.component';
import { ForgotPasswordComponent } from './Core/Components/user/forgot-password/forgot-password.component';
import { ResetpasswordComponent } from './Core/Components/user/resetpassword/resetpassword.component';
import { NotesComponent } from './Core/Components/notes/notes.component';
import { RemindersComponent } from './Core/Components/reminders/reminders.component';
import { MainnoteComponent } from './Core/Components/mainnote/mainnote.component';
import { ArchiveComponent } from './Core/Components/archive/archive.component';
import { TrashComponent } from './Core/Components/trash/trash.component';
import { DisplayNotesComponent } from './Core/Components/display-notes/display-notes.component';
import { IconComponent } from './Core/Components/icon/icon.component';

const routes: Routes = [
  { path:'',redirectTo:'/user/login',pathMatch:'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'resetpassword', component: ResetpasswordComponent }
    ]
  },
  { path:'home',component:HomeComponent,canActivate:[AuthGuard]
,children:[
  {
    path:'',
    redirectTo:'note',
    pathMatch:'full'
  },
{
  path:'note',
  component:MainnoteComponent
},

{ 
  path: 'reminders', 
  component: RemindersComponent
 },
{
  path:'archieve',
  component:ArchiveComponent
},
{
  path:'trash',
  component:TrashComponent
},
{
  path:'Display',
  component:DisplayNotesComponent
},
{
  path:'icon',
  component:IconComponent
}
  
]


},
  // {
  //   path:'home',
  //  children:[
  //    { path:'notes', redirectTo:'./notes',pathMatch:'full'},
    
     
  //    { path: 'reminders', component: RemindersComponent }
  //  ]
  // }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }