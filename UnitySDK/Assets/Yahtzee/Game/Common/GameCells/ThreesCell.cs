namespace Yahtzee.Game.Common.GameCells
{
    public class ThreesCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            return EvaluateNumberCategoryCell(gameboard, hand, 3);
        }

        public override int MeanExpectation(Gameboard gameboard)
        {
            return 9;
        }
    }
}