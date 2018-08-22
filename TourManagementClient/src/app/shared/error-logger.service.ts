import { Injectable } from '@angular/core';

@Injectable()
export class ErrorLoggerService {

  logError(error: any): void {
    console.error(error);
  }
}
