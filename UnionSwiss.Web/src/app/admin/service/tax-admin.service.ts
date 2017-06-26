import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, RequestOptionsArgs } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { TaxYear } from '../../shared/types/taxyear'

@Injectable()
export class TaxAdminService {
  private url: string = 'http://localhost/UnionSwiss.Api';
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: Http) {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
  }

  public getTaxPeriodList(): Observable<TaxYear[]> {
    let route = `${this.url}/api/v1/taxyear`;

    return this.http.get(route, this.options)
      .map((response: Response) => <TaxYear[]>response.json())
      .do(data => console.log('All: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }



  handleError(error): any {
    console.log(error)
  }


} 
