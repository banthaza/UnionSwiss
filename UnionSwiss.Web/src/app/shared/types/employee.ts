import { PaySlip } from "./payslip"
export class Employee {
    id: number;
    firstName: string;
    lastName: string;
    annualSalary: number;
    pensionContributionPercentage: number;
    startDate: Date;
    paySlips: PaySlip[];
    constructor() {
        this.id = 0;
        this.firstName = '';
        this.lastName = '';
        this.annualSalary = 0;
        this.pensionContributionPercentage = 0;
        this.paySlips = []
    }

};

