import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraphStepComponent } from './graph-step.component';

describe('GraphStepComponent', () => {
  let component: GraphStepComponent;
  let fixture: ComponentFixture<GraphStepComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GraphStepComponent]
    });
    fixture = TestBed.createComponent(GraphStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
