import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { EmployeeService } from '../service/employee.service'
import { Employee } from '../../shared/types/employee'
import { PaySlip } from '../../shared/types/paySlip'
import { CreatePaySlipMessage } from '../../shared/types/createPaySlipMessage'

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styles: ['']
})
export class EmployeeDetailComponent implements OnInit {
  employee: Employee;
  constructor(private router: Router, private activeRoute: ActivatedRoute, private employeeService: EmployeeService) {
    this.employee = new Employee();
    this.onCreateNewPayslip = this.onCreateNewPayslip.bind(this);
  }


  ngOnInit() {
    console.log(JSON.stringify(this.activeRoute.params));
    this.activeRoute.params
      .switchMap((params: Params) =>
        this.employeeService.getEmployee(params['employeeId']))
      .subscribe((employee: Employee) => this.employee = employee,
      this.handleError);
  }

  onSave(): void {
    if (this.employee.id === 0) {
      this.employeeService.createEmployee(this.employee).subscribe(
        (employee) => this.handleEmployeeSaved, (error) => this.handleError);
    } else {
      this.employeeService.updateEmployee(this.employee).subscribe(
        (employee) => this.handleEmployeeSaved, (error) => this.handleError);
    }
  }

  onCreateNewPayslip(createPaySlipMessage: CreatePaySlipMessage) {
    console.log("this worked" + JSON.stringify(createPaySlipMessage));
    this.employeeService.createEmployeePayslip(createPaySlipMessage.employeeId, createPaySlipMessage.newPeriodDate)
    .subscribe(
      this.handlePayslipCreated,
      (error) => this.handleError);
  }

  handleEmployeeSaved(employee: Employee): void {
    if (this.employee.id !== employee.id) {
      this.router.navigate(['/employee', employee.id]);
    } else {
      this.employee = employee;
    }
  }

  handlePayslipCreated(payslip: PaySlip): void {
    if (payslip) {
      this.employee.paySlips.push(payslip);
    }
  }

  handleError(error): void {
    console.log(error);
  }
}
