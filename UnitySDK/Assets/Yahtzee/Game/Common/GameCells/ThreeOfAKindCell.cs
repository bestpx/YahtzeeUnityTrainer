namespace Yahtzee.Game.Common.GameCells
{
    public class ThreeOfAKindCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            int value = 0;

            if (hand.HasThreeOfAKind())
            {
                value += hand.GetSum();
            }

            value += YahtzeeBonus(gameboard, hand);
            return value;
        }
    }
}