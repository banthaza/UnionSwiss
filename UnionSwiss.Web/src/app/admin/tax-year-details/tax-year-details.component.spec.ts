import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxYearDetailsComponent } from './tax-year-details.component';

describe('TaxYearDetailsComponent', () => {
  let component: TaxYearDetailsComponent;
  let fixture: ComponentFixture<TaxYearDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaxYearDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaxYearDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
