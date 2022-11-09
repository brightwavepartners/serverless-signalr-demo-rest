import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignalrService } from './core/services/signalr/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  //#region fields

  private readonly _apiBaseUrl: string = "https://localhost:7048/api/"; // points to api app service

  message: string = '';
  title: string = 'angular-signalr-demo';

  //#endregion

  //#region constructors

  constructor(
    private httpClient: HttpClient,
    private signalRService: SignalrService) {
  }

  //#endregion

  //#region methods

  ngOnInit(): void {
    this.signalRService.ngOnInit();
  }

  public broadcast(): void {
    this.signalRService.broadcast(this.message).subscribe();
    this.message = '';
  }

  public startWork(): any {
    return this.httpClient.post(`${this._apiBaseUrl}work`, { Id: 1 }).subscribe((data: any) => {console.log(data)});
  }

  //#endregion
}
