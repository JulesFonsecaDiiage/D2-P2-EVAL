import { Injectable } from '@angular/core';
import { WebApiService } from './web-api.service';
import { environment } from '../../environments/environment';

const apiUrl = environment.apiUrl;
const passwordsUrl = `${apiUrl}passwords`;
const applicationsUrl = `${apiUrl}applications`;

@Injectable({
  providedIn: 'root'
})
export class HttpProviderService {
  constructor(private webApiService: WebApiService) { }

  public getPasswords() {
    return this.webApiService.get(passwordsUrl);
  }

  public addPassword(passwordData: any) {
    return this.webApiService.post(passwordsUrl, passwordData);
  }

  public deletePassword(id: string) {
    return this.webApiService.delete(`${passwordsUrl}/${id}`);
  }

  public getApplications() {
    return this.webApiService.get(applicationsUrl);
  }

  public addApplication(applicationData: any) {
    return this.webApiService.post(applicationsUrl, applicationData);
  }
}
