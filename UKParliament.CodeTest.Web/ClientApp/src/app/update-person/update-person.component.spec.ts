import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MaterialModule } from 'src/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatePersonComponent } from './update-person.component';

describe('UpdatePersonComponent', () => {
  let component: UpdatePersonComponent;
  let fixture: ComponentFixture<UpdatePersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdatePersonComponent ],
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

    fixture = TestBed.createComponent(UpdatePersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
