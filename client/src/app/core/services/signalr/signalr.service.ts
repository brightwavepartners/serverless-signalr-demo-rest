// http://yuezhizizhang.github.io/asp.net/signalr/websocket/2020/02/10/how-to-turn-signalr-events-into-observable.html

import { BroadcastMessage } from "./broadcastmessage";
import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from "@angular/core";
import { NewConnection } from "./newconnection";
import { WorkUpdateMessage } from "./workupdatemessage";

import * as signalR from "@microsoft/signalr";
import { map, Observable } from "rxjs";


@Injectable({
    providedIn: 'root'
})
export class SignalrService implements OnInit {
    //#region fields

    private readonly _hubBaseUrl: string = "http://localhost:7159/api/"; // points to azure function app "hub"
    private readonly _connection: signalR.HubConnection;
    
    //#endregion

    //#region constructors

    constructor(private http: HttpClient) {
        console.log('SignalRService constructor called');

        this._connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this._hubBaseUrl}`)
            .build();
    }

    //#endregion

    //#region methods

    ngOnInit(): void {
        console.log('SignalRService ngOnInit called');

        this.start();

        this._connection.on('onBroadcastMessage', this.onBroadcastMessage);
        this._connection.on('newConnection', this.onNewConnection);
        this._connection.on('onWorkUpdateMessage', this.onWorkUpdateMessage);
    }

    private onBroadcastMessage(broadcastMessage: BroadcastMessage): void {
        console.log(`Broadcast message received: ${broadcastMessage.Text}`);
    }

    private onNewConnection(connection: NewConnection) {
        console.log(`New connection with id: ${connection.ConnectionId}`);
    }
    
    private onWorkUpdateMessage(workUpdateMessage: WorkUpdateMessage): void {
        console.log(`Work update message received: ${workUpdateMessage.Value}`);
    }

    private async start() {
        try {
            await this._connection.start();
            console.log("SignalR connected");
        } catch (err) {
            console.log(err);
        }
    }
 
    public broadcast(message: string): Observable<Object> {       
        let requestUrl = `${this._hubBaseUrl}broadcast`;
        return this.http.post(requestUrl, message);
    }

    //#endregion
}