import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameViewModel } from '../viewmodels/GameViewModel';
import { deserialize } from 'json-typescript-mapper';

import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    GameId: number;
    GameViewModel: GameViewModel;

    constructor(
        private route: ActivatedRoute,
        private dataService: DataService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.GameId = params['Id'];

            this.dataService.GetGame(this.GameId)
                .subscribe(
                (data) => {
                    this.GameViewModel = deserialize(GameViewModel, data);
                },
                (error) => {
                    console.log(error);
                }
                );
        });
    }

}
