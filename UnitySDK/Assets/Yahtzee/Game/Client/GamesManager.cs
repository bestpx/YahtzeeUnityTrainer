using System;
using CommonUtil;
using UnityEngine;
using UnityEngine.Analytics;
using Yahtzee.Game.MLAgent;
using ILogger = CommonUtil.ILogger;

namespace Yahtzee.Game.Client
{
    public class GamesManager : MonoBehaviour
    {
        private int _gameCount = 0;
        private float _averageScore = 0;
        
        private ILogger Logger
        {
            get { return ServiceFactory.GetService<ILogger>(); }
        }
        
        public void AddAgent(YahtzeePlayerAgent agent)
        {
            agent.GameOverEvent += ProcessGameOverEvent;
        }

        private void ProcessGameOverEvent(GameOverEvent evt)
        {
            float score = evt.Game.GetScore();
            int gameCountNext = _gameCount + 1;
            _averageScore = (_averageScore * _gameCount / gameCountNext) + (score / gameCountNext);
            _gameCount = gameCountNext;
            
            Logger.Log(LogLevel.Info, "GameOver with score: " + score + "\n Current Average: " + _averageScore);
            Logger.Log(LogLevel.Info, "_gameCount: " + _gameCount);
        }
    }
}