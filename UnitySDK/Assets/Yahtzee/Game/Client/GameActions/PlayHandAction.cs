namespace Yahtzee.Game.Client.GameActions
{
    public class PlayHandAction : GameAction
    {
        /// <summary>
        /// CellId to play hand in
        /// </summary>
        private readonly int _cellId;
        public PlayHandAction(int cellId) : base()
        {
            _cellId = cellId;
        }
        
        public override void Perform(Common.Game game)
        {
            game.PlayHand(_cellId);
        }

        public override void Revert(Common.Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}