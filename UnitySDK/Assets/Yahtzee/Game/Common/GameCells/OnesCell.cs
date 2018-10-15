namespace Yahtzee.Game.Common.GameCells
{
    public class OnesCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            return EvaluateNumberCategoryCell(gameboard, hand, 1);
        }
    }
}