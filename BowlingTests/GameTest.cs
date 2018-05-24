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

        private IRunningGame StartTestGame(byte playerCount = 1)
        {
            Enumerable.Range(0, playerCount).ToList().ForEach(x => gameBuilder.AddPlayer(new Player()));
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
        public void ScoreCanBeRetrievedAfterRoll()
        {
            Player player = new Player();
            gameBuilder.AddPlayer(player);
            var runningGame = gameBuilder.Start();

            runningGame.Roll(5);

            Assert.AreEqual(5, player.Score);
        }

        [TestMethod]
        public void FrameIsFinished_NextPlayerIsActive()
        {
            var player1 = new Player();
            var player2 = new Player();
            gameBuilder.AddPlayer(player1);
            gameBuilder.AddPlayer(player2);
            var game = gameBuilder.Start();

            game.Roll(5);
            game.Roll(1);

            Assert.AreEqual(player2, game.ActivePlayer);
        }

        [TestMethod]
        public void RoundIsFinished_FirstPlayerIsActive()
        {
            var player1 = new Player();
            var player2 = new Player();
            gameBuilder.AddPlayer(player1);
            gameBuilder.AddPlayer(player2);
            var game = gameBuilder.Start();

            game.Roll(5);
            game.Roll(1);

            game.Roll(2);
            game.Roll(3);

            Assert.AreEqual(player1, game.ActivePlayer);


        }
    }
}
