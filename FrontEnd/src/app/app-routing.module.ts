import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicantsComponent } from './modules/applicants/applicants.component';
import { EmployeeDetailsComponent } from './modules/employees/employee-details/employee-details.component';
import { EmployeesComponent } from './modules/employees/employees-overview/employees.component';
import { GreetingComponent } from './modules/greeting/greeting.component';

const routes: Routes = [
  { path: 'applicants', component: ApplicantsComponent },
  { path: '', component: GreetingComponent },
  { path: 'employees/:id', component: EmployeeDetailsComponent },
  { path: 'employees', component: EmployeesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
