
export class TaxBracket {
    id: number;
    taxYearId: number;
    minQualifyingValue: number;
    maxQualifyingValue: number;
    baseTaxValue: number;
    incrementMultiplier: number;
    constructor() {
        this.id = 0;
        this.taxYearId = 0;
        this.minQualifyingValue = 0;
        this.maxQualifyingValue = 0;
        this.baseTaxValue = 0;
        this.incrementMultiplier = 0;
    }
};
