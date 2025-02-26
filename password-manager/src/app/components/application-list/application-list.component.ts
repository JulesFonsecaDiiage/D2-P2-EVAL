import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { HttpProviderService } from '../../services/http-provider.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-application-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './application-list.component.html',
  styleUrl: './application-list.component.scss'
})
export class ApplicationListComponent implements OnInit {
  apps: any[] = [];

  constructor(private httpProvider: HttpProviderService, private router: Router) {}

  ngOnInit() {
    this.loadApps();
  }

  loadApps() {
    this.httpProvider.getApplications().subscribe((data) => {
      this.apps = data;
    });
  }

  goToAddApplication() {
    this.router.navigate(['/applications/add']);
  }
}