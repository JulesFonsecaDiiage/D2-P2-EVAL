import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpProviderService } from '../../services/http-provider.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-application',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-application.component.html',
  styleUrl: './add-application.component.scss'
})
export class AddApplicationComponent {
  appData = { name: '', type: ''};
  types: {
    label: string;
    value: string;
  }[] = [
    { label: 'Grand Public', value: 'GrandPublic' },
    { label: 'Professionnelle', value: 'Professionnelle' }
  ];

  constructor(private httpProvider: HttpProviderService, private router: Router) {}

  addApplication() {
    this.httpProvider.addApplication(this.appData).subscribe(() => {
      this.router.navigate(['/applications']);
    });
  }
}