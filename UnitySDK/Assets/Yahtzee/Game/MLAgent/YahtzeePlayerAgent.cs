using System;
using System.Collections.Generic;
using CommonUtil;
using MLAgents;
using NUnit.Framework;
using UnityEngine;
using Yahtzee.Game.Client;
using Yahtzee.Game.Client.GameActions;
using ILogger = CommonUtil.ILogger;

namespace Yahtzee.Game.MLAgent
{
    public class YahtzeePlayerAgent : Agent
    {
        public event Action<GameOverEvent> GameOverEvent = delegate {  };
        private YahtzeeAcademy _academy;
        public float timeBetweenDecisionsAtInference = 0.1f;
        private float timeSinceDecision;
        
        private Common.Game _game;

        private List<GameAction> _actionTable;

        private ILogger Logger
        {
            get { return ServiceFactory.GetService<ILogger>(); }
        }
            
        public override void InitializeAgent()
        {
            Logger.Log(LogLevel.Info, "Initialize Agent");
            _game = ServiceFactory.GetService<GameService>().CreateNewGame();
            _academy = FindObjectOfType<YahtzeeAcademy>();
            var gamesManager = FindObjectOfType<GamesManager>();
            gamesManager.AddAgent(this);
            
            _actionTable = new List<GameAction>()
            {
                new GameActionRoll(),
            };
            
            AddPlayHandActions();
            AddToggleHoldDiceActions();
        }

        private void AddPlayHandActions()
        {
            for (int i = 1; i < 14; i++)
            {
                _actionTable.Add(new PlayHandAction(i));
            }
        }

        /// <summary>
        /// There should be a total of 31 choices
        /// </summary>
        private void AddToggleHoldDiceActions()
        {
            AddToggleHoldDiceActionsRecur(new bool[5], 0);
        }

        private void AddToggleHoldDiceActionsRecur(bool[] array, int index)
        {
            bool[] localArray = new bool[5];
            bool[] localArray2 = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                localArray[i] = array[i];
                localArray2[i] = array[i];
            }
            
            if (index == localArray.Length)
            {
                // don't hold all dice
                if (AllTrue(array))
                {
                    return;
                }
                _actionTable.Add(new ToggleHoldDiceAction(localArray));
                return;
            }

            localArray[index] = true;
            localArray2[index] = false;
            AddToggleHoldDiceActionsRecur(localArray, index + 1);
            AddToggleHoldDiceActionsRecur(localArray2, index + 1);
        }

        private bool AllTrue(bool[] array)
        {
            bool value = true;
            for (int i = 0; i < array.Length; i++)
            {
                value = value && array[i];
            }

            return value;
        }
    
        public override void CollectObservations()
        {
            // observe gameboard
            for (int i = 1; i < 14; i++)
            {
                AddVectorObs(_game.GetScoreInCell(i));
            }
           
            // observe hand
            for (int i = 0; i < 5; i++)
            {
                AddVectorObs(_game.GetDiceAt(i));
            }
            for (int i = 0; i < _game.Hand.Deck.Length; i++)
            {
                AddVectorObs(_game.Hand.Deck[i]);
            }
            // observe gameboard cells, this affects the available decisions
            for (int i = 1; i < 14; i++)
            {
                AddVectorObs(_game.CanPlayInCell(i));
            }
            AddVectorObs(_game.CanRoll());
            AddVectorObs(_game.CanToggle());
            AddVectorObs(_game.Hand.RollCount);
            
            // mask actions
            List<int> mask = new List<int>();
            for (int i = 0; i < _actionTable.Count; i++)
            {
                if (!_actionTable[i].IsValid(_game))
                {
                   mask.Add(i);
                }
            }

            if (mask.Count != _actionTable.Count)
            {
                SetActionMask(0, mask);
            }
        }
    
        public override void AgentAction(float[] vectorAction, string textAction)
        {
            int actionIndex = (int)vectorAction[0];
            int scoreCurrentTurn = 0;
            var gameAction = _actionTable[actionIndex];
            int expectation = gameAction.MeanExpectation(_game);
            if (brain.brainParameters.vectorActionSpaceType == SpaceType.discrete)
            {
                int scoreBefore = _game.GetScore();
                // do actions

                // Is there better ways to enforce game rule?
                if (!gameAction.IsValid(_game))
                {
                    return;
                }
                _actionTable[actionIndex].Perform(_game);
                
                // parse action result
                int scoreAfter = _game.GetScore();
                scoreCurrentTurn = scoreAfter - scoreBefore;
            }
            
            // Reward agent
            SetReward(scoreCurrentTurn - expectation);
            
            if (_game.IsGameOver()) // gameover
            {
                EndTraining();
            }
        }

        private void EndTraining()
        {
            GameOverEvent(new GameOverEvent(_game));
            Logger.Log(LogLevel.Debug, "Game finished with score: " + _game.GetScore());
            Done();
        }
    
        public override void AgentReset()
        {
            Logger.Log(LogLevel.Debug, "AgentReset");
            _game = ServiceFactory.GetService<GameService>().CreateNewGame();
        }

        private void Update()
        {
            if (!_academy.GetIsInference())
            {
                RequestDecision();
            }
            else
            {
                if (timeSinceDecision >= timeBetweenDecisionsAtInference)
                {
                    timeSinceDecision = 0f;
                    brain.brainParameters.vectorActionSize[0] = _actionTable.Count;
                    RequestDecision();
                }
                else
                {
                    timeSinceDecision += Time.deltaTime;
                }
            }
        }
    }
}