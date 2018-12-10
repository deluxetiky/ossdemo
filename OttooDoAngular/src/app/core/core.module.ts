import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { PropertiesService } from './services/properties.service';
import { SignalRService } from './services/signal-r.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
  ],
  exports: [],
  providers: [
    PropertiesService,
    SignalRService
  ],
})
export class CoreModule { }
