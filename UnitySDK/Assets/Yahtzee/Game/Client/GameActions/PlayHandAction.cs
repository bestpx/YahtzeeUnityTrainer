using UnityEngine;

namespace Yahtzee.Game.Client.GameActions
{
    public class PlayHandAction : GameAction
    {
        /// <summary>
        /// CellId to play hand in
        /// </summary>
        private readonly int _cellId;
        public PlayHandAction(int cellId) : base()
        {
            _cellId = cellId;
        }

        public override bool IsValid(Common.Game game)
        {
            return game.CanPlayInCell(_cellId);
        }

        public override void Perform(Common.Game game)
        {
            int score = game.GetScore();
            game.PlayHand(_cellId);
            Debug.Log("Playing hand on cell: " + game.GetCellTypeAt(_cellId).Name + ", score: " + (game.GetScore() - score));
        }

        public override void Revert(Common.Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}