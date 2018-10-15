namespace Yahtzee.Game.Client
{
    /// <summary>
    /// Represent a meaningful action in game that player can perform. e.g. roll, lock dice, submit score
    /// </summary>
    public abstract class GameAction
    {
        public abstract bool IsValid(Common.Game game);
        public abstract void Perform(Common.Game game);
        public abstract void Revert(Common.Game game);
    }
}