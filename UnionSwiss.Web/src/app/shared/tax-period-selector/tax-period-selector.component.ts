import { Component, OnInit, Inject } from '@angular/core';
import { MD_DIALOG_DATA } from '@angular/material';
import { CreatePaySlipMessage } from '../../shared/types/createPaySlipMessage'
import { PaySlip } from '../../shared/types/payslip'

@Component({
  selector: 'app-tax-period-selector',
  templateUrl: './tax-period-selector.component.html',
  styles: []
})
export class TaxPeriodSelectorComponent implements OnInit {
  constructor( @Inject(MD_DIALOG_DATA) public createPaySlipMessage: CreatePaySlipMessage) {
    console.log(createPaySlipMessage);
  }

  ngOnInit() {

  }

}
