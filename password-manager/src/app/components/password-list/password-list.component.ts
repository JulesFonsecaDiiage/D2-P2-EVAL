import { Component, OnInit } from '@angular/core';
import { HttpProviderService } from '../../services/http-provider.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-password-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './password-list.component.html',
  styleUrl: './password-list.component.scss'
})
export class PasswordListComponent implements OnInit {
  passwords: any[] = [];

  constructor(private httpProvider: HttpProviderService, private router: Router) {}

  ngOnInit() {
    this.loadPasswords();
  }

  loadPasswords() {
    this.httpProvider.getApplications().subscribe((applications) => {
      this.httpProvider.getPasswords().subscribe((data) => {
        data.forEach((password: any) => {
          let application = applications.find((app: any) => app.id === password.applicationId);
          password.applicationName = application.name;
        });

        this.passwords = data;
      });
    });
  }

  deletePassword(id: string) {
    this.httpProvider.deletePassword(id).subscribe(() => {
      this.loadPasswords();
    });
  }

  goToAddPassword() {
    this.router.navigate(['/passwords/add']);
  }
}
