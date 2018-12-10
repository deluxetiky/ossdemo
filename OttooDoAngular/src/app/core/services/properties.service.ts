import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

const { version: appVersion } = require('../../../../package.json');

declare let require: any;

@Injectable()
export class PropertiesService {
  production: boolean;
  apiUrl: string;

  constructor() {
    this.production = environment.production;
    this.apiUrl = environment.apiUrl;

  }
}
