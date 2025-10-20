import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { managerTableGuard } from './manager-table.guard';

describe('managerTableGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => managerTableGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
