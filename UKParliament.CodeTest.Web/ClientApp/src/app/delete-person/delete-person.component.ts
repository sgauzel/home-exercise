import { PersonViewModel } from './../../models/person-view-model';
import { HttpClient } from '@angular/common/http';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import { ResponseViewModel } from '../../models/response-view-model';

@Component({
  selector: 'app-delete-person',
  templateUrl: './delete-person.component.html',
  styleUrls: ['./delete-person.component.scss']
})
export class DeletePersonComponent implements OnInit {

  message: string;
  person: PersonViewModel;
  isSuccess: boolean = true;

  constructor(
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string,
    public dialogRef: MatDialogRef<DeletePersonComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PersonViewModel) {
    
    this.person = data;
    this.message = data.name;
  }

  ngOnInit() {
  }

  onConfirm(): void {
    this.http.put<ResponseViewModel>(this.baseUrl + `api/person/Delete`, this.person).subscribe(result => {
      this.isSuccess = result.isSuccess;
      if (result.isSuccess) {
        this.dialogRef.close(true);
      }
   })
  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }

}
