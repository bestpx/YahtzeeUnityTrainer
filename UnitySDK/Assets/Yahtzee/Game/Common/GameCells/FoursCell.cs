namespace Yahtzee.Game.Common.GameCells
{
    public class FoursCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            return EvaluateNumberCategoryCell(gameboard, hand, 4);
        }
    }
}