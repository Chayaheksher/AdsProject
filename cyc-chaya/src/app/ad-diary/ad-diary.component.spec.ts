import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdDiaryComponent } from './ad-diary.component';

describe('AdDiaryComponent', () => {
  let component: AdDiaryComponent;
  let fixture: ComponentFixture<AdDiaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdDiaryComponent]
    });
    fixture = TestBed.createComponent(AdDiaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
