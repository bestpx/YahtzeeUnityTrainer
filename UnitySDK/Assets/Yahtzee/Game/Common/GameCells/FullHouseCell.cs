namespace Yahtzee.Game.Common.GameCells
{
    public class FullHouseCell : GameCell
    {
        public const int FullHouseValue = 25;
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            int value = 0;
            if (hand.IsFullHouse())
            {
                value += FullHouseValue;
            }
            value += YahtzeeBonus(gameboard, hand);
            return value;
        }

        public override int MeanExpectation(Gameboard gameboard)
        {
            return 20;
        }
    }
}