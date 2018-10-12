using System.Collections.Generic;
using System.Linq;
using Yahtzee.Game.Common.GameCells;

namespace Yahtzee.Game.Common
{
    /// <summary>
    /// Represents the layout and status of the current gameboard
    /// </summary>
    public abstract class Gameboard
    {
        protected List<GameCell> GameCells = new List<GameCell>();
        
        protected Gameboard(){}

        /// <summary>
        /// Should player be qualified for yahtzee bonus
        /// </summary>
        /// <returns></returns>
        public virtual bool ShouldHaveYahtzeeBonus()
        {
            // are all yahtzee cells filled with score
            bool hasPlayedAllYahtzee = true;
            var yahtzeeCells = GetAllCellsOfType<YahtzeeCell>();
            foreach (var yahtzeeCell in yahtzeeCells)
            {
                hasPlayedAllYahtzee = hasPlayedAllYahtzee && yahtzeeCell.HasPlayed();
            }

            return hasPlayedAllYahtzee;
        }

        private IEnumerable<GameCell> GetAllCellsOfType<T>() where T : GameCell
        {
            return GameCells.Where(cell => cell.GetType() == typeof(T));
        }

        public int YahtzeeBonus
        {
            get { return 50; }
        }
    }

    public class ClassicGameboard : Gameboard
    {
        public ClassicGameboard(List<GameCell> gameCells)
        {
            GameCells = gameCells;
        }
    }
    
    public class GameboardFactory
    {
        public static Gameboard MakeClassicGameboard()
        {
            var cellList = new List<GameCell>();
            cellList.Add(GameCellFactory.CreateOnesCell());
            return new ClassicGameboard(cellList);
        }
    }
}