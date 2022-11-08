import { DeletePersonComponent } from './../delete-person/delete-person.component';
import { UpdatePersonComponent } from './../update-person/update-person.component';
import { AddPersonComponent } from './../add-person/add-person.component';
import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from "@angular/common/http";
import {PersonViewModel} from "../../models/person-view-model";
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss'],
})
export class PersonComponent implements OnInit, OnDestroy {

  person$: BehaviorSubject<PersonViewModel[]> =  new BehaviorSubject(new Array<PersonViewModel>())

  addPersonDialog: MatDialogRef<AddPersonComponent> | undefined;
  updatePersonDialog: MatDialogRef<UpdatePersonComponent> | undefined;
  deletePersonDialog: MatDialogRef<DeletePersonComponent> | undefined;


  constructor(private http: HttpClient, 
              @Inject('BASE_URL') private baseUrl: string,
              private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadData();
    this.person$.subscribe();
  }

  
  addNewPerson() {
    this.addPersonDialog = this.dialog.open(AddPersonComponent,{
      minHeight:'400px',
      minWidth:'300px',
    });

    this.addPersonDialog.afterClosed().subscribe(result => {
      this.loadData();
      });
  }

  editRecord(person: PersonViewModel) {
    this.updatePersonDialog = this.dialog.open(UpdatePersonComponent,{
      minHeight:'400px',
      minWidth:'300px',
      data: person
    });

    this.updatePersonDialog.afterClosed().subscribe(result => {
      if (result) {
        this.loadData();
      }
    });
  }

  deleteRecord(person: PersonViewModel) {
    this.deletePersonDialog = this.dialog.open(DeletePersonComponent, {
      maxWidth: "400px",
      data: person
    });

    this.deletePersonDialog.afterClosed().subscribe(result => {
      if (result) {
        this.loadData();
      }
    });
  }

  private loadData() {

   this.http.get<PersonViewModel[]>(this.baseUrl + `api/person/GetAll`)
    .subscribe({
      next: result => this.person$.next(result), 
      error: error => console.log(error)
    })
  } 
  
  ngOnDestroy(){
    this.person$.unsubscribe();
  }
 
}
