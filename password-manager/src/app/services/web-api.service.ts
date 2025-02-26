import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/internal/operators/catchError';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WebApiService {
  private apiKey = environment.apiKey;

  constructor(private httpClient: HttpClient) { }

  // Get call method
  get(url: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'x-api-key': this.apiKey
      })
    };

    return this.httpClient.get(url, httpOptions).pipe(
      map((response: any) => this.ReturnResponseData(response)),
      catchError(this.handleError)
    );
  }

  // Post call method
  post(url: string, model: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'x-api-key': this.apiKey
      })
    };
  
    return this.httpClient.post(url, model, httpOptions).pipe(
      map((response: any) => this.ReturnResponseData(response)),  // Ajout du return
      catchError(this.handleError)
    );
  }

  // Delete call method
  delete(url: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'x-api-key': this.apiKey
      })
    };

    return this.httpClient.delete(url, httpOptions).pipe(
      map((response: any) => this.ReturnResponseData(response)),
      catchError(this.handleError)
    );
  }


  private ReturnResponseData(response: any) {
    return response?.body ? response.body : response;
  }

  private handleError(error: any) {
    return throwError(error);
  }

  postFormUrlEncoded(url: string, body: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/x-www-form-urlencoded'
      })
    };

    return this.httpClient.post(url, body, httpOptions).pipe(
      map((response: any) => this.ReturnResponseData(response)),
      catchError(this.handleError)
    );
  }
}