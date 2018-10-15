using System.Collections.Generic;
using CommonUtil;
using MLAgents;
using NUnit.Framework;
using UnityEngine;
using Yahtzee.Game.Client;
using Yahtzee.Game.Client.GameActions;

namespace Yahtzee.Game.MLAgent
{
    public class YahtzeePlayerAgent : Agent
    {
        private Common.Game _game;

        private List<GameAction> _actionTable;
            
        public override void InitializeAgent()
        {
            _actionTable = new List<GameAction>()
            {
                new GameActionRoll(),
            };
            
            AddPlayHandActions();
            AddToggleHoldDiceActions();
            
            Debug.Log(_actionTable.Count);
            Assert.IsTrue(_actionTable.Count == 14 + 31);
        }

        private void AddPlayHandActions()
        {
            for (int i = 1; i < 14; i++)
            {
                _actionTable.Add(new PlayHandAction(i));
            }
        }

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
            // TODO observe gameboard
            AddVectorObs(gameObject.transform.rotation.z);
            // TODO observe hand
        }
    
        public override void AgentAction(float[] vectorAction, string textAction)
        {
            int actionIndex = (int)vectorAction[0];
            int reward = 0;
            if (brain.brainParameters.vectorActionSpaceType == SpaceType.discrete)
            {
                int scoreBefore = _game.GetScore();
                // do actions
                _actionTable[actionIndex].Perform(_game);
                
                // parse action result
                int scoreAfter = _game.GetScore();
                reward = scoreAfter - scoreBefore;
            }
            
            // Reward agent
            SetReward(reward);
            if (_game.IsGameOver()) // gameover
            {
                Done();
            }
        }
    
        public override void AgentReset()
        {
            Debug.Log("AgentReset");
            _game = ServiceFactory.GetService<GameService>().CreateNewGame();
        }
    }
}