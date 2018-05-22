using System;
using System.Linq;
using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTests
{
    [TestClass]
    public class GameTest
    {
        private GameBuilder gameBuilder;

        private IRunningGame StartTestGame()
        {
            gameBuilder.AddPlayer(new Player());
            var runningGame = gameBuilder.Start();
            return runningGame;
        }

        [TestInitialize]
        public void Setup()
        {
            gameBuilder = new GameBuilder();
        }

        [TestMethod]
        public void ThereIsNoPlayerInAgame()
        {
            Assert.AreEqual(0, gameBuilder.Players.Count());
        }

        [TestMethod]
        public void CanAddPlayerToGame()
        {
            var player = new Player();

            gameBuilder.AddPlayer(player);

            Assert.IsTrue(gameBuilder.Players.Contains(player));
        }

        [TestMethod]
        public void AddNullPlayer_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => gameBuilder.AddPlayer(null));
            Assert.AreEqual("player", exception.ParamName);
        }

        [TestMethod]
        public void AddSamePlayerAgain_Throws()
        {
            var player = new Player();
            gameBuilder.AddPlayer(player);
            Assert.ThrowsException<PlayerAlreadyExistException>(() => gameBuilder.AddPlayer(player));
        }

        [TestMethod]
        public void CanStartGame()
        {
            var runningGame = StartTestGame();

            Assert.IsInstanceOfType(runningGame, typeof(IRunningGame));
        }

        [TestMethod]
        public void WhenThereAreNoPlayer_GameCannotBeStarted()
        {
            Assert.ThrowsException<InvalidOperationException>(gameBuilder.Start);
        }

        [TestMethod]
        public void WhenThereIsAtLeastOnePlayer_GameCanBeStarted()
        {
            var runningGame = StartTestGame();

            Assert.IsInstanceOfType(runningGame, typeof(IRunningGame));
        }
        
        [TestMethod]
        public void ScoreCanBeRetrieveAfterRoll()
        {
            Player player = new Player();
            gameBuilder.AddPlayer(player);
            var runningGame = gameBuilder.Start();

            runningGame.Roll(5);

            Assert.AreEqual(5, player.Score);
        }

        // JÓZSI: the next test is adding two players; the first one rolls twice, and the next active player should be the second one
    }
}
