import { TaxPeriod } from './taxperiod'
import { TaxBracket } from './taxbracket'
export class TaxYear {
    id: number;
    startDate: Date;
    endDate: Date;
    name: string;
    taxPeriods: TaxPeriod[];
    taxBrackets: TaxBracket[];
    constructor() {
        this.id = 0;
        this.startDate = new Date();
        this.endDate = new Date();
        this.name = "..."
        this.taxPeriods = new Array<TaxPeriod>()
        this.taxBrackets = new Array<TaxBracket>()
    }
};
