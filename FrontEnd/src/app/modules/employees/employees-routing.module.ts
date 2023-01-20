import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdmissionsCommitteeGuard } from 'src/app/core/components/admissions-committee.guard';
import { InternalServerComponent } from 'src/app/core/components/error-pages/internal-server/internal-server.component';
import { NotFoundComponent } from 'src/app/core/components/error-pages/not-found/not-found.component';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';
import { EmployeesComponent } from './employees-overview/employees.component';

const routes: Routes = [
  {
    path: 'employees/:id',
    component: EmployeeDetailsComponent,
    canActivate: [AdmissionsCommitteeGuard],
  },
  { path: '', component: EmployeesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EmployeesRoutingModule {}
