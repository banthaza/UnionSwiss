import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxPeriodSelectorComponent } from './tax-period-selector.component';

describe('TaxPeriodSelectorComponent', () => {
  let component: TaxPeriodSelectorComponent;
  let fixture: ComponentFixture<TaxPeriodSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaxPeriodSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaxPeriodSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
