using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Bowling
{
    internal class RunningGame : IRunningGame
    {
        public Player ActivePlayer => scoreBoard[currentFrameIndex].Player;
        public bool IsLastRollOfGame => currentFrameIndex == scoreBoard.Count - 1;
        public bool IsFirstRollOfPlayer => NextPlayerToRoll == ActivePlayer;
        private Player NextPlayerToRoll => IsLastRollOfGame ? null : scoreBoard[currentFrameIndex + 1].Player;
        public IList<Player> Players { get; }
        public string Winner => GetWinner();
        private bool IsGameFinished => currentFrameIndex >= scoreBoard.Count;

        private string GetWinner()
        {
            if (!IsGameFinished)
                throw new GameIsNotFinishedYetException();

            return Players.OrderByDescending(GetScoreOf).First().Name;
        }

        private readonly List<Frame> scoreBoard;
        private int currentFrameIndex = 0;

        public RunningGame(IList<Player> players)
        {
            scoreBoard = CreateScoreBoard(players);
            Players = players.ToImmutableList();
        }

        public void Roll(int pins)
        {
            if (IsGameFinished)
                throw new GameAlreadyFinishedException();

            var currentFrame = scoreBoard[currentFrameIndex];
            currentFrame.Roll(pins);

            foreach (var pendingStrike in GetPendingFrames())
                pendingStrike.AddBonus(pins);

            if (currentFrame.IsFinished)
                currentFrameIndex++;
        }

        private IEnumerable<Frame> GetPendingFrames()
        {
            return scoreBoard
                .Take(currentFrameIndex)
                .Where(x => x.Player == ActivePlayer && x.HasRemainingBonus);
        }

        private static List<Frame> CreateScoreBoard(IList<Player> p)
        {
            var newScoreBoard = new List<Frame>();
            for (int i = 0; i < 10; i++)
            {
                foreach (var player in p)
                {
                    newScoreBoard.Add(new Frame(player));                   
                }
            }
            return newScoreBoard;
        }

        public int GetScoreOf(Player player)
        {
            return scoreBoard
                .Where(r => r.Player == player && r.Score.HasValue)
                .Sum(r => r.Score.Value);
        }

        public int GetScoreOf(string name)
        {
            return GetScoreOf(Players.Single(p => p.Name == name));
        }

    }
}