import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpProviderService } from '../../services/http-provider.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-password',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-password.component.html',
  styleUrl: './add-password.component.scss'
})
export class AddPasswordComponent implements OnInit {
  passwordData = { accountName: '', password: '', applicationId: '' };
  applications: any[] = [];

  constructor(private httpProvider: HttpProviderService, private router: Router) {}

  ngOnInit() {
    this.httpProvider.getApplications().subscribe((apps) => (this.applications = apps));
  }

  addPassword() {
    this.httpProvider.addPassword(this.passwordData).subscribe(() => {
      this.router.navigate(['/passwords']);
    });
  }
}
