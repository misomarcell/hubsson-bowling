using System;
using System.Linq;
using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTests
{
    [TestClass]
    public class GameTest
    {
        private Game game;

        [TestInitialize]
        public void Setup()
        {
            game = new Game();
        }

        [TestMethod]
        public void ThereIsNoPlayerInAgame()
        {
            Assert.AreEqual(0, game.Players.Count());
        }

        [TestMethod]
        public void CanAddPlayerToGame()
        {
            var player = new Player();

            game.AddPlayer(player);

            Assert.IsTrue(game.Players.Contains(player));
        }

        [TestMethod]
        public void AddNullPlayer_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => game.AddPlayer(null));
            Assert.AreEqual("player", exception.ParamName);
        }

        [TestMethod]
        public void IsPlayerAlreadyExist()
        {
            var player = new Player();
            game.AddPlayer(player);
            Assert.ThrowsException<PlayerAlreadyExistException>(() => game.AddPlayer(player));
        }
    }
}
