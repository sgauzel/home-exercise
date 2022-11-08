import { BehaviorSubject } from 'rxjs';
import { ResponseViewModel } from '../../models/response-view-model';
import { PersonViewModel } from './../../models/person-view-model';
import { HttpClient } from '@angular/common/http';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, NgForm, Validators} from '@angular/forms';

@Component({
  selector: 'app-add-person',
  templateUrl: './add-person.component.html',
  styleUrls: ['./add-person.component.scss']
})
export class AddPersonComponent implements OnInit {

  addform: FormGroup;
  isSuccess: boolean = true;

  constructor(
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddPersonComponent>
  ) {
    dialogRef.disableClose = true;

    this.addform = this.fb.group({
      name: ['', [Validators.required, Validators.pattern('[a-zA-Z]+([a-zA-Z ]+)*')]],
      email: [null, [Validators.required, Validators.email]],
      address: ['', Validators.required],
      dateOfBirth: ['', Validators.required]
    });
  }
 

  ngOnInit() {
  }

  submit(form: NgForm) {
    this.http.post<ResponseViewModel>(this.baseUrl + `api/person/Add`,this.addform.value).subscribe(result => {
     this.isSuccess = result.isSuccess;
     if (result.isSuccess) {
      this.dialogRef.close({
        clicked: 'submit',
        form: form,
      });
     }
   })
  }
 
}


