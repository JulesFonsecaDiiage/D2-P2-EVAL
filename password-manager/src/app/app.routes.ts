import { Routes } from '@angular/router';
import { AddApplicationComponent } from './components/add-application/add-application.component';
import { AddPasswordComponent } from './components/add-password/add-password.component';
import { ApplicationListComponent } from './components/application-list/application-list.component';
import { PasswordListComponent } from './components/password-list/password-list.component';

export const routes: Routes = [
    { path: '', redirectTo: 'passwords', pathMatch: 'full' },
    { path: 'passwords', component: PasswordListComponent, title: 'Liste des mots de passe' },
    { path: 'passwords/add', component: AddPasswordComponent, title: 'Ajout de mot de passe' },
    { path: 'applications', component: ApplicationListComponent, title: 'Liste des applications' },
    { path: 'applications/add', component: AddApplicationComponent, title: 'Ajout d\'application' },
    { path: '**', redirectTo: 'passwords', pathMatch: 'full' },
];
