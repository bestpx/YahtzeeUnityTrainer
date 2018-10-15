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
        protected Dictionary<int, GameCell> GameCells = new Dictionary<int, GameCell>();
        protected Gameboard(){}

        #region queries
        public int GetScore()
        {
            int score = 0;
            foreach (var gameCell in GameCells)
            {
                score += gameCell.Value.Score;
            }

            return score;
        }
        
        public bool AllCellsPlayed()
        {
            bool ret = true;
            foreach (var gameCell in GameCells)
            {
                ret = ret && gameCell.Value.HasPlayed();
            }

            return ret;
        }

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
            return GameCells.Values.Where(cell => cell.GetType() == typeof(T));
        }

        public int YahtzeeBonus => 50;
        #endregion
        
        #region Actions

        public void PlayHand(int cellId, Hand hand)
        {
            GameCell cell;
            if (GameCells.TryGetValue(cellId, out cell))
            {
                if (!cell.HasPlayed())
                {
                    cell.PlayHand(hand, this);
                }
            }
        }
        #endregion
    }

    public class ClassicGameboard : Gameboard
    {
        public ClassicGameboard(Dictionary<int, GameCell> gameCells)
        {
            GameCells = gameCells;
        }
    }
    
    public class GameboardFactory
    {
        public static Gameboard MakeClassicGameboard()
        {
            var cellList = new Dictionary<int, GameCell>();
            cellList[1] = GameCellFactory.CreateCell<OnesCell>();
            cellList[2] = GameCellFactory.CreateCell<TwosCell>();
            cellList[3] = GameCellFactory.CreateCell<ThreesCell>();
            cellList[4] = GameCellFactory.CreateCell<FoursCell>();
            cellList[5] = GameCellFactory.CreateCell<FivesCell>();
            cellList[6] = GameCellFactory.CreateCell<SixesCell>();
            cellList[7] = GameCellFactory.CreateCell<ThreeOfAKindCell>();
            cellList[8] = GameCellFactory.CreateCell<FourOfAKindCell>();
            cellList[9] = GameCellFactory.CreateCell<FullHouseCell>();
            cellList[10] = GameCellFactory.CreateCell<SmallStraighCell>();
            cellList[11] = GameCellFactory.CreateCell<LargeStraightCell>();
            cellList[12] = GameCellFactory.CreateCell<YahtzeeCell>();
            cellList[13] = GameCellFactory.CreateCell<ChanceCell>();
            return new ClassicGameboard(cellList);
        }
    }
}