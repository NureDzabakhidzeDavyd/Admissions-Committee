import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SpecialityDetailsComponent } from './speciality-details/speciality-details.component';
import { SpecialityComponent } from './speciality-overview/specialities.component';

const routes: Routes = [
  { path: '', component: SpecialityComponent },
  { path: 'speciality/:id', component: SpecialityDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SpecialityRoutingModule {}
