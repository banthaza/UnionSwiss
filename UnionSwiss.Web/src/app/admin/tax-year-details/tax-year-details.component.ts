import { Component, OnInit, Inject } from '@angular/core';
import { MD_DIALOG_DATA } from '@angular/material';
import { TaxBracket } from '../../shared/types/taxBracket'
import { TaxYear } from '../../shared/types/taxYear'
@Component({
  selector: 'app-tax-year-details',
  templateUrl: './tax-year-details.component.html',
  styles: ['']
})
export class TaxYearDetailsComponent implements OnInit {
  newYearsDay: Date = new Date;
  taxBrackets: TaxBracket[];
  constructor() {
    let oldDate = new Date();
    this.taxBrackets = new Array<TaxBracket>();
    this.newYearsDay = new Date(oldDate.getFullYear(), oldDate.getMonth(), oldDate.getDate() + 1);
  }
  ngOnInit() {
  }

  onAddNewBracketClicked() {
    let taxBracket = new TaxBracket()
    taxBracket.maxQualifyingValue = 0;
    if (this.taxBrackets.length > 0) {
      taxBracket.minQualifyingValue + 1;
    }
    this.taxBrackets.push(taxBracket)
  }
}
