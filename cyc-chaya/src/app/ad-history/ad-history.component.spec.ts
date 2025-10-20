import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdHistoryComponent } from './ad-history.component';

describe('AdHistoryComponent', () => {
  let component: AdHistoryComponent;
  let fixture: ComponentFixture<AdHistoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdHistoryComponent]
    });
    fixture = TestBed.createComponent(AdHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
