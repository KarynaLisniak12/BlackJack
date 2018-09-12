var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
import { JsonProperty } from 'json-typescript-mapper';
var GameViewModel = /** @class */ (function () {
    function GameViewModel() {
        this.Bots = [];
    }
    __decorate([
        JsonProperty('Id'),
        __metadata("design:type", Number)
    ], GameViewModel.prototype, "GameId", void 0);
    __decorate([
        JsonProperty('Stage'),
        __metadata("design:type", Number)
    ], GameViewModel.prototype, "Stage", void 0);
    __decorate([
        JsonProperty({ clazz: PlayerViewModel, name: 'Human' }),
        __metadata("design:type", PlayerViewModel)
    ], GameViewModel.prototype, "Human", void 0);
    __decorate([
        JsonProperty({ clazz: PlayerViewModel, name: 'Dealer' }),
        __metadata("design:type", PlayerViewModel)
    ], GameViewModel.prototype, "Dealer", void 0);
    __decorate([
        JsonProperty({ clazz: PlayerViewModel, name: 'Bot' }),
        __metadata("design:type", Array)
    ], GameViewModel.prototype, "Bots", void 0);
    return GameViewModel;
}());
export { GameViewModel };
//# sourceMappingURL=GameViewModel.js.map