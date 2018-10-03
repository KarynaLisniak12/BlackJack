import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserNameService } from 'app/shared/services/user-name-service/user-name.service';
import { HttpService } from 'app/shared/services/http-service/http.service';
import { ErrorService } from 'app/shared/services/error-service/error.service';
import { AuthorizedUserView } from 'app/shared/models/authorized-user-view';

@Component({
  selector: 'app-startpage',
  templateUrl: './startpage.component.html',
  styleUrls: ['./startpage.component.css']
})
export class StartpageComponent implements OnInit {
    UserName: string;
    Player: AuthorizedUserView = new AuthorizedUserView();
    AmountOfBots: number = 0;
    GameId: number;

    constructor (
        private _userNameService: UserNameService,
        private _httpService: HttpService,
        private _errorService: ErrorService,
        private _router: Router
    ) { }

    ngOnInit() {
        this.UserName = this._userNameService.GetUserName();
        this.AuthUser(this.UserName);
    }

    AuthUser(userName: string) {
        this._httpService.GetAuthorizedPlayer(this.UserName)
            .subscribe(
            (data: AuthorizedUserView) => {
                this.Player.Name = data.Name;
                this.Player.PlayerId = data.PlayerId;
                this.Player.ResumeGame = data.ResumeGame;
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
        );
    }

    StartNewGame() {
        this._httpService.CreateGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(
            (data) => {
                this.GameId = data["GameId"];
                this._router.navigate(['/user/' + this.UserName + '/game/' + this.GameId]);
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
        );
    }

    ResumeGame() {
        this._httpService.ResumeGame(this.Player.PlayerId)
            .subscribe(
                (data) => {
                    this.GameId = data["GameId"];
                    this._router.navigate(['/user/' + this.UserName + '/game/' + this.GameId]);
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }
}