import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';

import { HttpService } from 'app/shared/services/http.service';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    GameId: number;
    Game: GameMappingModel = new GameMappingModel();

    BetValidationMessage: string;
    BetValidationError: boolean = false;
    Bet: number = 50;
    RoundResult: string;
    GameResult: string;

    BetInput: boolean = false;
    TakeCard: boolean = false;
    BlackJackDangerChoice: boolean = false;
    EndRound: boolean = false;
    EndGame: boolean = false;

    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _httpService: HttpService
    ) { }

    ngOnInit() {
        this._route.params.subscribe(params => {
            this.GameId = params['Id'];
            this.GetGame();
        });
    }

    GamePlayInitializer() {
        if (this.Game.Stage == 0) {
            this.GamePlayBetInput();
        }

        if (this.Game.Stage == 1) {
            this.ResumeAfterStartRound();
        }

        if (this.Game.Stage == 2) {
            this.ResumeAfterContinueRound();
        }
    }

    GetGame() {
        this._httpService.GetGame(this.GameId)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameMappingModel, data);

                    if (data["IsGameOver"] != "") {
                        this.GameResult = data["IsGameOver"];
                        this.GamePlayEndGame();
                    }

                    this.GamePlayInitializer();
                }
            );
    }

    ResumeAfterStartRound() {
        this._httpService.ResumeAfterStartRound(this.Game.Id)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameMappingModel, data);
                    this.StartRoundGamePlay(data["BlackJackChoice"], data["CanTakeCard"]);
                }
            );
    }

    ResumeAfterContinueRound() {
        this._httpService.ResumeAfterContinueRound(this.Game.Id)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameMappingModel, data);
                    this.ContinueRoundGamePlay(data["RoundResult"]);
                }
            );
    }

    AddCard(takeCard: boolean) {
        if (takeCard) {
            this._httpService.AddCard(this.Game.Id)
                .subscribe(
                (data) => {
                    if (data["CanTakeCard"]) {
                        this.Game.Human = deserialize(PlayerMappingModel, data);
                    }

                    if (!data["CanTakeCard"]) {
                        this.ContinueRound(false);
                    }
                }
            );
        }

        if (!takeCard) {
            this.ContinueRound(false);
        }
    }

    StartRoundGamePlay(blackJackChoice: boolean, canTakeCard: boolean) {
        this.Game.Stage = 1;
        this.BetValidationError = false;
        if (blackJackChoice) {
            this.GamePlayBlackJackDangerChoice();
        }
        if (canTakeCard) {
            this.GamePlayTakeCard();
        }
        if (!blackJackChoice && !canTakeCard) {
            this.ContinueRound(false);
        }
    }

    ContinueRoundGamePlay(roundResult: string) {
        this.RoundResult = roundResult;
        this.GamePlayEndRound();
    }

    StartRound() {
        this._httpService.StartRound(this.Game.Id, this.Game.Human.GamePlayerId, this.Bet)
            .subscribe(
            (data) => {
                this.Game = deserialize(GameMappingModel, data["Data"]);

                if (data["Message"] != "") {
                    this.ShowValidationMessage(data["Message"]);
                }
                if (data["Message"] == "") {
                    this.StartRoundGamePlay(data["Data"]["BlackJackChoice"], data["Data"]["CanTakeCard"]);
                }
            }
        );
    }

    ShowValidationMessage(validationMessage: string) {
        this.BetValidationError = true;
        this.BetValidationMessage = validationMessage;
    }

    ContinueRound(continueRound: boolean) {
        this._httpService.ContinueRound(this.Game.Id, continueRound)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameMappingModel, data);
                    this.ContinueRoundGamePlay(data["RoundResult"]);
                }
            );
    }

    StartNewGame() {
        this._httpService.EndGame(this.Game.Id, this.GameResult)
            .subscribe(
                (data) => {
                    this._router.navigate(['/user/' + this.Game.Human.Name]);
                }
            );
    }

    StartNewRound() {
        this._httpService.EndRound(this.Game.Id)
            .subscribe(
                (data) => {
                    this.GetGame();
                }
            );
    }

    GamePlayBetInput() {
        this.BetInput = true;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    }

    GamePlayTakeCard() {
        this.BetInput = false;
        this.TakeCard = true;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    }

    GamePlayBlackJackDangerChoice() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = true;
        this.EndRound = false;
        this.EndGame = false;
    }

    GamePlayEndRound() {
        this.Game.Stage = 2;
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = true;
        this.EndGame = false;
    }

    GamePlayEndGame() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = true;
    }
}
