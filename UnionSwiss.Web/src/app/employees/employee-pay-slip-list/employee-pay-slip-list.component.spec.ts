import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeePaySlipListComponent } from './employee-pay-slip-list.component';

describe('EmployeePaySlipListComponent', () => {
  let component: EmployeePaySlipListComponent;
  let fixture: ComponentFixture<EmployeePaySlipListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeePaySlipListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeePaySlipListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
