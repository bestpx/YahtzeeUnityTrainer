namespace Yahtzee.Game.Common.GameCells
{
    public class SmallStraighCell : GameCell
    {
        public const int SmallStraightValue = 30;
        
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            int value = 0;

            if (hand.GetMaxStreak() >= 3)
            {
                value += SmallStraightValue;
            }

            value += YahtzeeBonus(gameboard, hand);
            return value;
        }
    }
}