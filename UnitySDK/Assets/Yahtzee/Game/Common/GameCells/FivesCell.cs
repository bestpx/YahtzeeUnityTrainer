namespace Yahtzee.Game.Common.GameCells
{
    public class FivesCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            return EvaluateNumberCategoryCell(gameboard, hand, 5);
        }

        public override int MeanExpectation(Gameboard gameboard)
        {
            return 15;
        }
    }
}