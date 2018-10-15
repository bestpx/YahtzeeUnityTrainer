namespace Yahtzee.Game.Client.GameActions
{
    public class ToggleHoldDiceAction : GameAction
    {
        /// <summary>
        /// The toggle status of the hand
        /// </summary>
        /// <returns></returns>
        private readonly bool[] _toggle;

        /// <summary>
        /// Set hand dice lock status to _toggle
        /// </summary>
        public ToggleHoldDiceAction(bool[] toggle)
        {
            _toggle = new bool[toggle.Length];
            for (int i = 0; i < toggle.Length; i++)
            {
                _toggle[i] = toggle[i];
            }
        }
        
        public override void Perform(Common.Game game)
        {
            game.ToggleHand(_toggle);
        }

        public override void Revert(Common.Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}