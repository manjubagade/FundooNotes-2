import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { Console } from '@angular/core/src/console';
import { UserComponent } from '../user/user.component';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { EditComponent } from '../edit/edit.component';
import { IconComponent } from '../icon/icon.component';
import { NoteService } from '../../services/NoteService/note.service';
import { DataService } from '../../services/DataService/data.service';
import { MatChipInputEvent } from '@angular/material';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Observable } from 'rxjs';



@Component({
  selector: 'app-display-notes',
  templateUrl: './display-notes.component.html',
  styleUrls: ['./display-notes.component.css']
})
export class DisplayNotesComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes = [ENTER, COMMA];
  constructor(private service: NoteService, private dataService: DataService, route: Router, private toastr: ToastrService, public dialog: MatDialog) {

  }
  grid;
  @Input() cards;
  flag = true;
  css = 'row wrap'
  ngOnInit() {
    var Profilepic = localStorage.getItem("profilePic");
    console.log("Display Profile pic", Profilepic);

    var profile = localStorage.getItem('profilePic');

    this.dataService.currentMessage.subscribe(data => {
      console.log(data);
      console.log(this.css);

      this.css = data ? 'row wrap' : 'column'

      this.flag = data;
    });

  }

  openDialog(note) {
    console.log(note);
    // const dialogRef = this.dialog.open(EditNotes);
    const dialogRef = this.dialog.open(EditComponent,
      {
        data: note,
        //  height:auto,
        //  width:'auto
      });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'change') {
        console.log("take action here");
      }
      else {
        console.log("execute");
        console.log(note);

        console.log(note.id);
        this.service.UpdateNotes(note, note.id).subscribe(data => {
          console.log(data);
          // this.Delete.emit({});
        }, err => {
          console.log(err);
        })
      }
    })
  }

  remove(note, id) {
    note.label = null;
    console.log("00000000000" + note.label, id);
    this.service.UpdateNotes(note, id).subscribe(data => {
      console.log("88888888" + note, id)
    })
  }
}