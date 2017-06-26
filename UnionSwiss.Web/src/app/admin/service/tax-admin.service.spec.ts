import { TestBed, inject } from '@angular/core/testing';

import { TaxAdminService } from './tax-admin.service';

describe('TaxAdminService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TaxAdminService]
    });
  });

  it('should be created', inject([TaxAdminService], (service: TaxAdminService) => {
    expect(service).toBeTruthy();
  }));
});
