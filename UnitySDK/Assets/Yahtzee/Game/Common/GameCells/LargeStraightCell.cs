namespace Yahtzee.Game.Common.GameCells
{
    public class LargeStraightCell : GameCell
    {
        public const int LargeStraightValue = 40;
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            int value = 0;

            if (hand.GetMaxStreak() >= 3)
            {
                value += LargeStraightValue;
            }

            value += YahtzeeBonus(gameboard, hand);
            return value;
        }

        public override int MeanExpectation(Gameboard gameboard)
        {
            return 25;
        }
    }
}