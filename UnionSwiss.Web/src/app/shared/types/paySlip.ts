import { TaxPeriod } from "./taxPeriod"
export class PaySlip {
    id: number;
    employeeId: number;
    fullName: string;
    payPeriod: string;
    grossIncome: number;
    incomeTax: number;
    netIncome: number;
    pension: number;
    taxPeriod: TaxPeriod;

};
