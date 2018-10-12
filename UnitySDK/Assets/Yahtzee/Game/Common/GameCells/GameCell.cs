namespace Yahtzee.Game.Common.GameCells
{
    public abstract class GameCell
    {
        protected GameCell(){}
        public int CellId { get; set; }
        /// <summary>
        /// score in the gamecell, -1 for not played
        /// </summary>
        public int Score = -1;
        
        /// <summary>
        /// Evaluate score by hand and gameboard status
        /// </summary>
        /// <returns></returns>
        public abstract int EvaluateScore(Hand hand, Gameboard gameboard);

        public void PlayHand(Hand hand, Gameboard gameboard)
        {
            Score = EvaluateScore(hand, gameboard);
        }

        public bool HasPlayed()
        {
            return Score != -1;
        }
    }
}