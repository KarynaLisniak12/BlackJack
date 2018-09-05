﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;

namespace BlackJack.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<LogViewModel>> GetAll()
        {
            IEnumerable<Log> logs = await _logRepository.GetAll();
            IEnumerable<LogViewModel> logViewModels = LogToLogViewModel(logs);
            return logViewModels;
        }

        private IEnumerable<LogViewModel> LogToLogViewModel(IEnumerable<Log> logs)
        {
            List<LogViewModel> logViewModels = new List<LogViewModel>();

            foreach (Log log in logs)
            {
                LogViewModel logViewModel = new LogViewModel();
                logViewModel.Id = log.Id;
                logViewModel.GameId = log.GameId;
                logViewModel.DateTime = log.DateTime;
                logViewModel.Message = log.Message;
                logViewModels.Add(logViewModel);
            }

            return logViewModels;
        }
    }
}