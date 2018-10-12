import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from 'app/shared/modules/shared.module';
import { AppRoutingModule } from 'app/app-routing.module';
import { APP_BASE_HREF } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from 'app/app.component';
import { HomepageComponent } from 'app/homepage/homepage.component';
import { ErrorPageComponent } from 'app/error-page/error-page.component';

import { UserNameService } from 'app/shared/services/user-name-service/user-name.service';
import { ErrorService } from 'app/shared/services/error-service/error.service';
import { HttpService } from 'app/shared/services/http-service/http.service';
import { RequestInterceptor } from 'app/shared/interceptors/request-interceptor/request-interceptor';

@NgModule({
    declarations: [
        AppComponent,
        HomepageComponent,
        ErrorPageComponent
    ],
    imports: [
        BrowserModule,
        SharedModule,
        AppRoutingModule
    ],
    providers: [
        UserNameService,
        ErrorService,
        HttpService,
        {
            provide: APP_BASE_HREF,
            useValue: '/'
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: RequestInterceptor,
            multi: true
        }
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
