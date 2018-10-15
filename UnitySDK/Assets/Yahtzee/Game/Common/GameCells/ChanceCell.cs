namespace Yahtzee.Game.Common.GameCells
{
    public class ChanceCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            int value = 0;
            value += hand.GetSum();
            value += YahtzeeBonus(gameboard, hand);
            return value;
        }
    }
}