import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
  },
  {
    path: 'organizations',
    loadComponent: () => import('./features/organizations/organizations.component').then(m => m.OrganizationsComponent)
  },
  {
    path: 'organizations/:id',
    loadComponent: () => import('./features/organizations/organization-detail.component').then(m => m.OrganizationDetailComponent)
  },
  {
    path: 'projects',
    loadComponent: () => import('./features/projects/projects.component').then(m => m.ProjectsComponent)
  },
  {
    path: 'projects/:id',
    loadComponent: () => import('./features/projects/project-detail.component').then(m => m.ProjectDetailComponent)
  },
  { path: '**', redirectTo: '' }
];
