import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditOrNewComponent } from './edit-or-new.component';

describe('EditOrNewComponent', () => {
  let component: EditOrNewComponent;
  let fixture: ComponentFixture<EditOrNewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditOrNewComponent]
    });
    fixture = TestBed.createComponent(EditOrNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
