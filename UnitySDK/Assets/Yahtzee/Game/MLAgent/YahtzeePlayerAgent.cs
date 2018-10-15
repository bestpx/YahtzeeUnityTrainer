using System.Collections.Generic;
using CommonUtil;
using MLAgents;
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
                new GameActionRoll(),
            };
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