import { TestBed } from '@angular/core/testing';

import { PresetationGoalsService } from './presetation-goals.service';

describe('PresetationGoalsService', () => {
  let service: PresetationGoalsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PresetationGoalsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
