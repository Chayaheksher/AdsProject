import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { userGouardGuard } from './user-gouard.guard';

describe('userGouardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => userGouardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
