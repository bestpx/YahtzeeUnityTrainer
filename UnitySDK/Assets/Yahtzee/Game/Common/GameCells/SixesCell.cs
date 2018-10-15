namespace Yahtzee.Game.Common.GameCells
{
    public class SixesCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            return EvaluateNumberCategoryCell(gameboard, hand, 6);
        }
    }
}