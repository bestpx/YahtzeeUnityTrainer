namespace Yahtzee.Game.Common
{
    /// <summary>
    /// Roll represents each dice roll
    /// </summary>
    public class RollSlot
    {
        public RollSlot(int rollValue, bool isLocked)
        {
            RollValue = rollValue;
            IsLocked = isLocked;
        }

        public void ToggleLock()
        {
            IsLocked = !IsLocked;
        }

        public int RollValue { get; set; }
        public bool IsLocked { get; set; }
    }
}