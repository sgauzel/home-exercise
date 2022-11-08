import { MaterialModule } from 'src/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePersonComponent } from './delete-person.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

describe('DeletePersonComponent', () => {
  let component: DeletePersonComponent;
  let fixture: ComponentFixture<DeletePersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeletePersonComponent ],
      imports: [HttpClientTestingModule, 
        ReactiveFormsModule,
        BrowserAnimationsModule,
        FormsModule, 
        MaterialModule,
      ],
  providers: [
    { provide: 'BASE_URL', useValue: 'http://localhost' },
    {provide: MatDialogRef, useValue: {}},
    {provide: MAT_DIALOG_DATA, useValue: []},
  ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeletePersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
