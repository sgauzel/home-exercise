import { ResponseViewModel } from '../../models/response-view-model';
import { HttpClient } from '@angular/common/http';
import { PersonViewModel } from './../../models/person-view-model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import {FormBuilder, FormGroup, NgForm, Validators} from '@angular/forms';

@Component({
  selector: 'app-update-person',
  templateUrl: './update-person.component.html',
  styleUrls: ['./update-person.component.scss']
})
export class UpdatePersonComponent implements OnInit {

  editform: FormGroup;
  isSuccess: boolean = true;

  constructor(
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) data: PersonViewModel,
    public dialogRef: MatDialogRef<UpdatePersonComponent>
  ) {
    dialogRef.disableClose = true;

    this.editform = this.fb.group({
      id: [data.id],
      name: [data.name, [Validators.required, Validators.pattern('[a-zA-Z]+([a-zA-Z ]+)*')]],
      email: [data.email, [Validators.required, Validators.email]],
      address: [data.address, Validators.required],
      dateOfBirth: [data.dateOfBirth, Validators.required]
    });
  }

  ngOnInit() {
  }

  submit(form: NgForm) {
    this.http.put<ResponseViewModel>(this.baseUrl + `api/person/Update`,this.editform.value).subscribe(result => {
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
