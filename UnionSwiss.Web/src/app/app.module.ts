import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '@angular/material';
import { MdDialogModule, MdDatepickerModule, MdNativeDateModule, } from '@angular/material';


import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import 'hammerjs';

//Components
import { HomeComponent } from './shared/home/home.component';
import { NavigationComponent } from './shared/navigation/navigation.component';
import { AdminComponent } from './admin/admin.component';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';
import { EmployeeDetailComponent } from './employees/employee-detail/employee-detail.component';
import { EmployeePaySlipComponent } from './employees/employee-payslip/employee-payslip.component';
import { EmployeePaySlipListComponent } from './employees/employee-pay-slip-list/employee-pay-slip-list.component';

//Service
import { EmployeeService } from './employees/service/employee.service';
import { TaxAdminService } from './admin/service/tax-admin.service';

//Pipes
import { MoneyPipe } from './shared/money.pipe';
import { PercentIntPipe } from './shared/percent-int.pipe';
import { TaxYearDetailsComponent } from './admin/tax-year-details/tax-year-details.component';
import { TaxPeriodSelectorComponent } from './shared/tax-period-selector/tax-period-selector.component';


@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent,
    EmployeeDetailComponent,
    EmployeePaySlipComponent,
    EmployeePaySlipListComponent,
    NavigationComponent,
    HomeComponent,
    MoneyPipe,
    PercentIntPipe,
    AdminComponent,
    TaxYearDetailsComponent,
    TaxPeriodSelectorComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MaterialModule,
    MdDatepickerModule,
    MdDialogModule,
    MdNativeDateModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, },
      { path: "employees", component: EmployeeListComponent },
      { path: "employees/:employeeId", component: EmployeeDetailComponent },
      { path: "admin", component: AdminComponent }
    ]),
    HttpModule,
    FlexLayoutModule
  ],
  entryComponents: [EmployeePaySlipComponent, TaxYearDetailsComponent, TaxPeriodSelectorComponent, TaxPeriodSelectorComponent],
  exports: [MaterialModule],
  providers: [EmployeeService, TaxAdminService],
  bootstrap: [AppComponent]
})
export class AppModule { }
