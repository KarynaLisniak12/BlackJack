var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthorizedUserComponent } from './authorized-user/authorized-user.component';
import { StartpageComponent } from './authorized-user/startpage/startpage.component';
var routes = [
    {
        path: '',
        component: AuthorizedUserComponent,
        children: [
            {
                path: '',
                component: StartpageComponent
            },
            {
                path: 'game/:Id',
                loadChildren: 'app/authorized-user/authorized-user/game/game.module#GameModule'
            }
        ]
    }
];
var AuthorizedUserRoutingModule = /** @class */ (function () {
    function AuthorizedUserRoutingModule() {
    }
    AuthorizedUserRoutingModule = __decorate([
        NgModule({
            imports: [
                RouterModule.forChild(routes)
            ],
            exports: [
                RouterModule
            ]
        })
    ], AuthorizedUserRoutingModule);
    return AuthorizedUserRoutingModule;
}());
export { AuthorizedUserRoutingModule };
//# sourceMappingURL=authorized-user-routing.module.js.map