import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { UserNameService } from 'app/shared/services/user-name.service';

@Component({
    selector: 'app-authorized-user',
    templateUrl: './authorized-user.component.html'
})
export class AuthorizedUserComponent implements OnInit {
    UserName: string;

    constructor(
        private _userNameService: UserNameService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route.params.subscribe(params => {
            this.UserName = params['UserName']
        });

        this._userNameService.SetUserName(this.UserName);
    }
}
