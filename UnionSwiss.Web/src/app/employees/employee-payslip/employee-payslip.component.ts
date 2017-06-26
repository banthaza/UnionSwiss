import { Component, OnInit, Inject } from '@angular/core';
import { MD_DIALOG_DATA } from '@angular/material';

import { PaySlip } from '../../shared/types/paySlip'

@Component({
  selector: 'app-employee-payslip',
  templateUrl: './employee-payslip.component.html',
  styles: [''],
})
export class EmployeePaySlipComponent implements OnInit {
  constructor( @Inject(MD_DIALOG_DATA) public paySlip: PaySlip) { }

  ngOnInit() {
  }

}
