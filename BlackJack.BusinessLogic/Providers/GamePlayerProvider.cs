﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.DataAccess.Repositories.Interfaces;

namespace BlackJack.BusinessLogic.Providers
{
    public class GamePlayerProvider : IGamePlayerProvider
    {
        private readonly ILogRepository _logRepository;
        private IGamePlayerRepository _gamePlayerRepository;


        public GamePlayerProvider(IGamePlayerRepository gamePlayerRepository, ILogRepository logRepository)
        {
            _logRepository = logRepository;
            _gamePlayerRepository = gamePlayerRepository;
        }

        public async Task BetsCreation(IEnumerable<GamePlayer> inGamePlayers, int bet)
        {
            List<GamePlayer> gamePlayers = inGamePlayers.ToList();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (gamePlayer.Player.IsHuman)
                {
                    gamePlayer.Bet = bet;
                }

                if (!gamePlayer.Player.IsDealer && !gamePlayer.Player.IsHuman)
                {
                    gamePlayer.Bet = BetGenerate(gamePlayer.Score);
                }

                if (!gamePlayer.Player.IsDealer)
                {
                    gamePlayer.Score = gamePlayer.Score - gamePlayer.Bet;
                    await _gamePlayerRepository.Update(gamePlayer);
                    await _logRepository.CreateLogBetIsCreated(inGamePlayers.First().GameId, gamePlayer.Player, gamePlayer.Score, gamePlayer.Bet);
                }
            }
        }

        public async Task RoundBetPayments(IEnumerable<GamePlayer> players)
        {
            GamePlayer human = players.Where(m => m.Player.IsHuman).FirstOrDefault();
            GamePlayer dealer = players.Where(m => m.Player.IsDealer).FirstOrDefault();

            foreach (GamePlayer player in players)
            {
                if (!player.Player.IsDealer && player.BetPayCoefficient != BetValueHelper.BetDefaultCoefficient)
                {
                    BetPayment(player, dealer);
                    await _gamePlayerRepository.Update(player);
                    await _gamePlayerRepository.Update(dealer);
                }
            }
        }

        private void BetPayment(GamePlayer player, GamePlayer dealer)
        {
            int pay = (int)(player.Bet * player.BetPayCoefficient);

            player.Score += player.Bet + pay;
            player.Bet = BetValueHelper.BetZero;
            player.BetPayCoefficient = BetValueHelper.BetDefaultCoefficient;

            dealer.Score -= pay;
        }

        private int BetGenerate(int playerScore)
        {
            Random random = new Random();

            int maxBet = BetValueHelper.BotMaxBet;
            if (maxBet > playerScore)
            {
                maxBet = playerScore;
            }

            int bet = (random.Next(maxBet / BetValueHelper.BetGenCoef) + 1) * BetValueHelper.BetGenCoef;
            return bet;
        }
    }
}