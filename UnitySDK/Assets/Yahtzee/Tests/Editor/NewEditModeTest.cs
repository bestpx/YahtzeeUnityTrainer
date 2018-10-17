using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using CommonUtil;
using Yahtzee;

public class NewEditModeTest {

    [Test]
    public void NewEditModeTestSimplePasses() 
    {
        // Use the Assert class to test conditions.
        var game = ServiceFactory.GetService<GameService>().CreateNewGame();
        
        // verify initial conditions
        Assert.IsTrue(!game.IsGameOver());
        Assert.IsTrue(game.GetScore() == 0);
        Assert.IsTrue(game.GetScoreInCell(1) == 0);
        Assert.IsTrue(game.GetScoreInCell(2) == 0);
        Assert.IsTrue(game.GetScoreInCell(3) == 0);
        Assert.IsTrue(game.GetScoreInCell(4) == 0);
        Assert.IsTrue(game.GetScoreInCell(5) == 0);
        Assert.IsTrue(game.GetScoreInCell(6) == 0);
        Assert.IsTrue(game.GetScoreInCell(7) == 0);
        Assert.IsTrue(game.GetScoreInCell(8) == 0);
        Assert.IsTrue(game.GetScoreInCell(9) == 0);
        Assert.IsTrue(game.GetScoreInCell(10) == 0);
        Assert.IsTrue(game.GetScoreInCell(11) == 0);
        Assert.IsTrue(game.GetScoreInCell(12) == 0);
        Assert.IsTrue(game.GetScoreInCell(13) == 0);
        Assert.IsTrue(game.CanRoll());
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewEditModeTestWithEnumeratorPasses() 
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
