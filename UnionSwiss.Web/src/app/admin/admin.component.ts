import { Component, OnInit } from '@angular/core';
import { MdDialog } from '@angular/material';
import { Router } from '@angular/router';

import { TaxAdminService } from './service/tax-admin.service'
import { TaxYearDetailsComponent } from './tax-year-details/tax-year-details.component'
import { TaxYear } from '../shared/types/taxyear'
@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styles: ['']
})
export class AdminComponent implements OnInit {
  taxYears: TaxYear[]
  maxEndDate: Date
  constructor(private taxAdminService: TaxAdminService, public dialog: MdDialog) {

  }

  ngOnInit() {
    this.taxAdminService.getTaxPeriodList()
      .subscribe(taxYears => {
        this.taxYears = taxYears;
        this.maxEndDate = this.taxYears.reduce(function (accumulator, currentValue) { return accumulator.endDate > currentValue.endDate ? accumulator : currentValue; }).endDate;

      },
      error => { console.log(error) });
  }

  onAddFinancialYearClicked() {
    let dialogRef = this.dialog.open(TaxYearDetailsComponent, { data: this.maxEndDate });
  }


}
