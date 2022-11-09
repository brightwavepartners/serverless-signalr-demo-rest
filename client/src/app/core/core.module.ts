import { NgModule, Optional, SkipSelf } from '@angular/core';
import {  SignalrService  } from './services/signalr/signalr.service';
 
@NgModule({
  imports: [
  ],
  providers: [
    SignalrService
  ],
  declarations: []
})
export class CoreModule { 
 
  constructor(@Optional() @SkipSelf() core:CoreModule ){
    if (core) {
        throw new Error("You should import core module only in the root module")
    }
  }
}