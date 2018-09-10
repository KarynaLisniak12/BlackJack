﻿using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using NLog;

namespace BlackJack.UI.Controllers
{
    [RoutePrefix("GameLogic")]
    public class GameLogicController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGameLogicService _gameLogicService;

        public GameLogicController (IGameLogicService gameLogicService)
        {
            _gameLogicService = gameLogicService;
        }

        [Route("RoundStart"), HttpGet]
        public async Task<IHttpActionResult> RoundStart(int inGameId)
        {
            try
            {
                await _gameLogicService.RoundFirstPhase(inGameId);
                return Json(new { Message = GameMessageHelper.Success });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("FirstPhaseGamePlay"), HttpGet]
        public async Task<IHttpActionResult> FirstPhaseGamePlay(int inGameId)
        {
            try
            {
                bool humanBjAndDealerBjDanger = await _gameLogicService.IsHumanBjAndDealerBjDanger(inGameId);
                bool canHumanTakeOneMoreCard = await _gameLogicService.CanHumanTakeOneMoreCard(inGameId);
                return Json(new { Message = GameMessageHelper.Success, HumanBjAndDealerBjDanger = humanBjAndDealerBjDanger, CanHumanTakeOneMoreCard = canHumanTakeOneMoreCard });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("HumanBjAndDealerBjDangerContinueRound"), HttpGet]
        public async Task<IHttpActionResult> HumanBjAndDealerBjDangerContinueRound(int inGameId)
        {
            try
            {
                await _gameLogicService.HumanBjAndDealerBjDangerContinueRound(inGameId);
                return Json(new { Message = GameMessageHelper.Success });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("AddOneMoreCardToHuman"), HttpGet]
        public async Task<IHttpActionResult> AddOneMoreCardToHuman(int inGameId)
        {
            try
            {
                await _gameLogicService.AddOneMoreCardToHuman(inGameId);
                bool canHumanTakeOneMoreCard = await _gameLogicService.CanHumanTakeOneMoreCard(inGameId);
                return Json(new { Message = GameMessageHelper.Success, CanHumanTakeOneMoreCard = canHumanTakeOneMoreCard });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("SecondPhase"), HttpGet]
        public async Task<IHttpActionResult> SecondPhase(int inGameId)
        {
            try
            {
                await _gameLogicService.RoundSecondPhase(inGameId);
                return Json(new { Message = GameMessageHelper.Success });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
    }
}