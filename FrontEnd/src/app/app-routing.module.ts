import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdmissionsCommitteeGuard } from './core/components/admissions-committee.guard';
import { InternalServerComponent } from './core/components/error-pages/internal-server/internal-server.component';
import { NotFoundComponent } from './core/components/error-pages/not-found/not-found.component';
import { LoginComponent } from './core/components/login/login.component';
import { LogoutComponent } from './core/components/logout/logout.component';
import { ApplicantsComponent } from './modules/applicants/applicants-overview/applicants.component';
import { EmployeeDetailsComponent } from './modules/employees/employee-details/employee-details.component';
import { EmployeesComponent } from './modules/employees/employees-overview/employees.component';
import { GreetingComponent } from './modules/greeting/greeting.component';
import { SpecialityDetailsComponent } from './modules/speciality/speciality-details/speciality-details.component';
import { SpecialityComponent } from './modules/speciality/speciality-overview/specialities.component';

const routes: Routes = [
  { path: '', component: GreetingComponent },
  {
    path: 'applicants',
    loadChildren: () =>
      import(`./modules/applicants/applicants.module`).then(
        (m) => m.ApplicantsModule
      ),
  },
  {
    path: 'employees',
    loadChildren: () =>
      import(`./modules/employees/employees.module`).then(
        (m) => m.EmployeesModule
      ),
  },
  {
    path: 'specialities',
    loadChildren: () =>
      import(`./modules/speciality/speciality.module`).then(
        (m) => m.SpecialityModule
      ),
  },

  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
