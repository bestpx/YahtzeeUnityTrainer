namespace Yahtzee.Game.Common.GameCells
{
    public abstract class GameCellFactory
    {
        public static OnesCell CreateOnesCell()
        {
            return new OnesCell();
        }
    }
}