using System;
using System.Text;
using UnityEngine;

namespace Yahtzee.Game.Common
{
    /// <summary>
    /// Contains gameboard state and other meta data for the game
    /// </summary>
    public class Game
    {
        private string _gameId;
        private Gameboard _gameboard;
        private Hand _hand;
        
        private Game() {}

        Game(string gameId, Gameboard gameboard)
        {
            _gameId = gameId;
            _gameboard = gameboard;
            _hand = new Hand();
        }

        #region queries
        public bool IsGameOver()
        {
            return _gameboard.AllCellsPlayed();
        }

        public int GetScore()
        {
            return _gameboard.GetScore();
        }

        public int GetScoreInCell(int cellId)
        {
            return _gameboard.GetScoreInCell(cellId);
        }

        public bool CanRoll()
        {
            return _hand.CanRoll();
        }

        public bool CanPlayInCell(int cellId)
        {
            return _hand.HasRolled() && !_gameboard.HasPlayedInCell(cellId);
        }

        public bool CanToggle()
        {
            return _hand.HasRolled() && !_hand.HasChangedLockedDiceSinceLastRoll;
        }

        public int GetDiceAt(int index)
        {
            return _hand.GetRollAt(index);
        }

        public Type GetCellTypeAt(int cellId)
        {
            return _gameboard.GetCellType(cellId);
        }
        
        #endregion

        #region actions
        public void Roll()
        {
            if (_hand.CanRoll())
            {
                _hand.Roll();
            }
            else
            {
                Debug.Log("Roll");
            }
        }

        public void PlayHand(int cellId)
        {
            _gameboard.PlayHand(cellId, _hand);
        }

        public void ToggleHand(bool[] toggle)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < toggle.Length; i++)
            {
                sb.Append(toggle[i]);
                sb.Append(",");
            }
            Debug.Log("ToggleHand: " + sb);
            _hand.SetLockStatus(toggle);
        }

        #endregion

        /// <summary>
        /// Factory method
        /// </summary>
        public static class GameFactory
        {
            public static Game MakeClassicGame()
            {
                var gameboard = GameboardFactory.MakeClassicGameboard();
                var guid = Guid.NewGuid();
                return new Game(guid.ToString(), gameboard);
            }
        }
    }
}