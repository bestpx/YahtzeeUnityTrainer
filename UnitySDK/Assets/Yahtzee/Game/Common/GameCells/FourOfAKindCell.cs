namespace Yahtzee.Game.Common.GameCells
{
    public class FourOfAKindCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            int value = 0;

            if (hand.HasFourOfAKind())
            {
                value += hand.GetSum();
            }

            value += YahtzeeBonus(gameboard, hand);
            return value;
        }
    }
}