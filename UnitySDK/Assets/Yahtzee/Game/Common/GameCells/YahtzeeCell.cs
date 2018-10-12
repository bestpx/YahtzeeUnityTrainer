namespace Yahtzee.Game.Common.GameCells
{
    public class YahtzeeCell : GameCell
    {
        public override int EvaluateScore(Hand hand, Gameboard gameboard)
        {
            int score = hand.GetNumberOfRollsOfValue(1);
            if (gameboard.ShouldHaveYahtzeeBonus())
            {
                score += gameboard.YahtzeeBonus;
            }

            return score;
        }
    }
}