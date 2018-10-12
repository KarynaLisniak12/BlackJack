﻿import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpResponse,
    HttpErrorResponse
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from "rxjs/operators";
import { ErrorService } from 'app/shared/services/error-service/error.service';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {

    constructor(
        private _errorService: ErrorService,
        private _router: Router
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(tap(
            (event: HttpEvent<any>) => { },
            (error: any) => {
                if (error instanceof HttpErrorResponse) {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            }
        ));
    }
}