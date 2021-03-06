﻿using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using BlackJack.ViewModels.ViewModels.Game;
using BlackJack.ViewModels.ViewModels.Start;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Mappers
{
    public static class CustomMapper
    {
        public static AuthorizePlayerViewModel GetAuthorizePlayerViewModel(Player human, Game game)
        {
            var authorizePlayerViewModel = new AuthorizePlayerViewModel()
            {
                PlayerId = human.Id,
                Name = human.Name,
                ResumeGame = true
            };

            if (game == null || !string.IsNullOrEmpty(game.Result))
            {
                authorizePlayerViewModel.ResumeGame = false;
            }

            return authorizePlayerViewModel;
        }

        public static InitRoundViewModel GetInitRoundViewModel(Game game, List<GamePlayer> players, string isGameOver)
        {
            InitRoundViewModel initRoundViewModel = Mapper.Map<Game, InitRoundViewModel>(game);
            initRoundViewModel.Bots = new List<PlayerItem>();

            foreach (GamePlayer player in players)
            {
                if (player.Player.Type == (int)PlayerType.Dealer)
                {
                    initRoundViewModel.Dealer = Mapper.Map<GamePlayer, PlayerItem>(player);
                }

                if (player.Player.Type == (int)PlayerType.Human)
                {
                    initRoundViewModel.Human = Mapper.Map<GamePlayer, PlayerItem>(player);
                }

                if (player.Player.Type == (int)PlayerType.Bot)
                {
                    PlayerItem bot = Mapper.Map<GamePlayer, PlayerItem>(player);
                    initRoundViewModel.Bots.Add(bot);
                }
            }

            return initRoundViewModel;
        }

        public static StartRoundResponseViewModel GetStartRoundResponseViewModel(List<GamePlayer> players, long gameId, bool canTakeCard, bool isBlackJackChoice)
        {
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var startRoundResponseViewModel = new StartRoundResponseViewModel();
            startRoundResponseViewModel.Dealer = Mapper.Map<GamePlayer, GamePlayerItem>(dealer);
            startRoundResponseViewModel.Dealer.RoundScore = GameValueHelper.Zero;
            startRoundResponseViewModel.Dealer.Cards.Clear();
            startRoundResponseViewModel.Dealer.Cards.Add(ToStringHelper.GetCardName(dealer.PlayerCards[0].Card));
            startRoundResponseViewModel.Human = Mapper.Map<GamePlayer, GamePlayerItem>(human);
            startRoundResponseViewModel.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerItem>>(players);
            startRoundResponseViewModel.CanTakeCard = canTakeCard;
            startRoundResponseViewModel.BlackJackChoice = isBlackJackChoice;
            startRoundResponseViewModel.Id = gameId;
            return startRoundResponseViewModel;
        }

        public static ContinueRoundResponseViewModel GetContinueRoundResponseViewModel(List<GamePlayer> players, long gameId, string humanRoundResult)
        {
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var continueRoundResponseViewModel = new ContinueRoundResponseViewModel();
            continueRoundResponseViewModel.Dealer = Mapper.Map<GamePlayer, GamePlayerItem>(dealer);
            continueRoundResponseViewModel.Human = Mapper.Map<GamePlayer, GamePlayerItem>(human);
            continueRoundResponseViewModel.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerItem>>(players);
            continueRoundResponseViewModel.RoundResult = humanRoundResult;
            continueRoundResponseViewModel.Id = gameId;
            return continueRoundResponseViewModel;
        }

        public static Player GetPlayer(string name, PlayerType playerType)
        {
            var player = new Player();
            player.Name = name;
            player.Type = (int)playerType;
            return player;
        }

        public static GamePlayer GetGamePlayer(Player player, long gameId)
        {
            var gamePlayer = new GamePlayer()
            {
                GameId = gameId,
                PlayerId = player.Id,
                Score = GameValueHelper.DefaultPlayerScore,
                BetPayCoefficient = BetValueHelper.DefaultCoefficient,
                Bet = GameValueHelper.Zero,
                RoundScore = GameValueHelper.Zero,
                Player = player
            };

            return gamePlayer;
        }
        
        public static PlayerCard GetPlayerCard(GamePlayer gamePlayer, Card card)
        {
            var playerCard = new PlayerCard() { GamePlayerId = gamePlayer.Id, CardId = card.Id, Card = card };
            return playerCard;
        }
    }
}
