namespace Yahtzee.Game.Common.GameCells
{
    public class YahtzeeCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            return hand.IsYahtzee() ? gameboard.YahtzeeBonus : 0;
        }
    }
}