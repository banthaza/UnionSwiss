import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, RequestOptionsArgs } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { Employee } from '../../shared/types/employee'

@Injectable()
export class EmployeeService {
  private url: string = 'http://localhost/UnionSwiss.Api';
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: Http) {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
  }

  public getEmployeeList(): Observable<Employee[]> {
    let route = `${this.url}/api/v1/employees`;


    return this.http.get(route, this.options)
      .map((response: Response) => <Employee[]>response.json())
      .do(data => console.log('All: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  public getEmployee(id: number): Observable<Employee> {
    console.log(id);
    let route = `${this.url}/api/v1/employees/${id}`;
    return this.http.get(route, this.options)
      .map((response: Response) => {
        return <Employee>response.json()
      })
      .do(data => console.log('Employee: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  public updateEmployee(employee: Employee): Observable<Employee> {
    let route = `${this.url}/api/v1/employees/${employee.id}`;
    return this.http.put(route, JSON.stringify(employee), this.options)
      .map((response: Response) => {
        return <Employee>response.json()
      })
      .do(data => console.log('Employee: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  public createEmployee(employee: Employee): Observable<Employee> {
    let route = `${this.url}/api/v1/employees`;
    return this.http.post(route, JSON.stringify(employee), this.options)
      .map((response: Response) => {
        return <Employee>response.json()
      })
      .do(data => console.log('Employee: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  createEmployeePayslip(employeeId: number, newPeriod: Date) {
    var dateString = `${newPeriod.getFullYear()}-${newPeriod.getMonth() + 1}-1` //+1 casue utc is messing me up.
    console.log(dateString);
    let route = `${this.url}/api/v1/employees/${employeeId}/payslip/${dateString}`;
    return this.http.post(route, "", this.options)
      .map((response: Response) => {
        return <Employee>response.json()
      })
      .do(data => console.log('Employee: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  handleError(error): any {
    console.log(error)
  }


} 
