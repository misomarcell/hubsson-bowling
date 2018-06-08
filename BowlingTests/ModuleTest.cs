using System;
using System.Linq;
using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTests
{
    [TestClass]
    public class ModuleTest
    {
        private GameBuilder gameBuilder;

        private IRunningGame StartTestGame(params string[] names)
        {
            foreach (var name in names)
            {
                gameBuilder.AddPlayer(name);
            }
            return gameBuilder.Start();
        }

        private IRunningGame StartTestGame(byte playerCount = 1)
        {
            Enumerable.Range(0, playerCount).ToList().ForEach(x => gameBuilder.AddPlayer($"Player {x}"));
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
            var game = StartTestGame("Józsi");

            Assert.IsTrue(game.Players.Any(p => p.Name == "Józsi"));
        }

        [TestMethod]
        public void AddNullPlayer_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => gameBuilder.AddPlayer((string)null));
            Assert.AreEqual("name", exception.ParamName);
        }

        [TestMethod]
        public void AddSamePlayerAgain_Throws()
        {
            Assert.ThrowsException<PlayerAlreadyExistException>(() => StartTestGame("Tomi", "Tomi"));
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
            var runningGame = StartTestGame("Sanyi");

            runningGame.Roll(5);

            Assert.AreEqual(5, runningGame.GetScoreOf("Sanyi"));
        }

        [TestMethod]
        public void FrameIsFinished_NextPlayerIsActive()
        {
            var game = StartTestGame("Norbi", "Attila");

            game.Roll(5);
            game.Roll(1);

            Assert.AreEqual("Attila", game.ActivePlayer.Name);
        }

        [TestMethod]
        public void RoundIsFinished_FirstPlayerIsActive()
        {
            var game = StartTestGame("Norbi", "Attila");

            game.Roll(5);
            game.Roll(1);

            game.Roll(2);
            game.Roll(3);

            Assert.AreEqual("Norbi", game.ActivePlayer.Name);
        }

        [TestMethod]
        public void FirstPlayerKeepsScoreAfterSecondRound()
        {
            var runningGame = StartTestGame(2);
            runningGame.Roll(5);
            runningGame.Roll(6);
            runningGame.Roll(7);

            Assert.AreEqual(11, runningGame.GetScoreOf(runningGame.Players[0]));
        }

        [TestMethod]
        public void ScoreCanBeAccessedByPlayerName()
        {
            var runningGame = StartTestGame("ReadyPlayerOne","Luigi");
            runningGame.Roll(5);
            runningGame.Roll(6);
            runningGame.Roll(7);

            Assert.AreEqual(11, runningGame.GetScoreOf("ReadyPlayerOne"));
        }

        [TestMethod]
        public void CheckWinnerNameAtTheEndOfTheGame()
        {
            var game = StartTestGame("Winner", "Loser");
            PlayFrames(game, 10, name => name == "Winner" ? 2 : 1);

            Assert.AreEqual("Winner", game.Winner);
        }

        [TestMethod]
        public void GameHasNotFinishedYet_WinnerIsUndetermined()
        {
            var game = StartTestGame("Sanyi", "Attila");

            Assert.ThrowsException<GameIsNotFinishedYetException>(() => game.Winner);
        }

        private static void PlayFrames(IRunningGame game, int frameCount, Func<string, int> scores)
        {
            int rollCount = 2 * frameCount * game.Players.Count;
            for (int rollIndex = 0; rollIndex < rollCount; rollIndex++)
            {
                game.Roll(scores(game.ActivePlayer.Name));
            }
        }
    }
}
