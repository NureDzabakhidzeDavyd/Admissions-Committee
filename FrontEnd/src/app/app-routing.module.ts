import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicantsComponent } from './modules/applicants/applicants.component';
import { EmployeeDetailsComponent } from './modules/employees/employee-details/employee-details.component';
import { EmployeesComponent } from './modules/employees/employees-overview/employees.component';
import { GreetingComponent } from './modules/greeting/greeting.component';
import { SpecialityDetailsComponent } from './modules/speciality/speciality-details/speciality-details.component';
import { SpecialityComponent } from './modules/speciality/speciality-overview/specialities.component';

const routes: Routes = [
  { path: '', component: GreetingComponent },
  { path: 'applicants', component: ApplicantsComponent },
  { path: 'employees/:id', component: EmployeeDetailsComponent },
  { path: 'employees', component: EmployeesComponent },
  { path: 'specialities', component: SpecialityComponent },
  { path: 'speciality/:id', component: SpecialityDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
