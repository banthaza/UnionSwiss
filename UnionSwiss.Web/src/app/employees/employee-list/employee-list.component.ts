import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from '../service/employee.service';
import { Employee } from  '../../shared/types/employee';
@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styles: ['']
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[]
  constructor(private router: Router, private employeeService: EmployeeService) { }

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getEmployeeList()
      .subscribe(employees => this.employees = employees,
      error => { console.log(error) });
  }

  onEditClicked(employee: Employee) {
    // Navigate with relative link
    this.router.navigate(['/employees', employee.id]);
  }

  onAddClicked() {
    // Navigate with relative link
    console.log("add");
    this.router.navigate(['/employees', 0]);
  }
}
