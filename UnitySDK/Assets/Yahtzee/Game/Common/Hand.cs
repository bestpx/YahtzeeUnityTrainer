namespace Yahtzee.Game.Common
{
    /// <summary>
    /// Represent a hand of given number of dice Rolls
    /// </summary>
    public class Hand
    {
        private const int HandSize = 5;
        
        /// <summary>
        /// size is 7, 1-indexed
        /// </summary>
        private int[] _histogram = new int[7];
        private Roll[] _rolls = new Roll[HandSize];

        public Hand(int[] rolls)
        {
            for (int i = 0; i < HandSize; i++)
            {
                _rolls[i] = new Roll();
                _histogram[rolls[i]]++;
            }
        }

        public int GetNumberOfRollsOfValue(int value)
        {
            return _histogram[value];
        }
    }
}