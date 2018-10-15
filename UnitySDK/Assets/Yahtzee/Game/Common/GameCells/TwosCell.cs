namespace Yahtzee.Game.Common.GameCells
{
    public class TwosCell : GameCell 
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            return EvaluateNumberCategoryCell(gameboard, hand, 2);
        }
    }
}