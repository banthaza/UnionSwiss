import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import { MdDialog } from '@angular/material';
import { Router } from '@angular/router';

import { TaxPeriodSelectorComponent } from '../../shared/tax-period-selector/tax-period-selector.component'
import { EmployeePaySlipComponent } from '../employee-payslip/employee-payslip.component'
import { PaySlip } from '../../shared/types/payslip'
import { CreatePaySlipMessage } from '../../shared/types/createPaySlipMessage'

@Component({
  selector: 'app-employee-pay-slip-list',
  templateUrl: './employee-pay-slip-list.component.html',
  styles: ['']

})
export class EmployeePaySlipListComponent implements OnInit {
  @Input() employeeId: number[];
  @Input() paySlips: PaySlip[];
  @Output() onCreateNewPayslip: EventEmitter<CreatePaySlipMessage> = new EventEmitter();
  constructor(private router: Router, public dialog: MdDialog) {
  }

  ngOnInit() {
    console.log(this.paySlips)
  }

  onViewPayslipClicked(payslip: PaySlip) {
    let dialogRef = this.dialog.open(EmployeePaySlipComponent, { data: payslip });
  }

  onViewAddPayslipClicked(payslip: PaySlip) {
    let today = new Date();
    let newPeriodDate = new Date(today.getFullYear(), today.getMonth(), 1);
    console.log(newPeriodDate)

    if (this.paySlips.length > 0) {
      let lastDate = new Date(this.paySlips.reduce(function (accumulator, currentValue) { return accumulator.taxPeriod.startDate > currentValue.taxPeriod.startDate ? accumulator : currentValue; }).taxPeriod.startDate);
      newPeriodDate = new Date(lastDate.getFullYear(), (lastDate.getMonth() + 1), 1);
    }

    let dialogRef = this.dialog.open(TaxPeriodSelectorComponent, { data: { newPeriodDate: newPeriodDate, employeeId: this.employeeId } });
    dialogRef.afterClosed().subscribe((result: CreatePaySlipMessage) => {
      if (result != null) {
        console.log("emitting" + JSON.stringify(result))
        this.onCreateNewPayslip.emit(result);
      }
    });
  }


}
