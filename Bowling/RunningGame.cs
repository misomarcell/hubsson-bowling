using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Bowling
{
    internal class RunningGame : IRunningGame
    {
        public Player ActivePlayer => scoreBoard[currentRollIndex].Player;
        public bool IsLastRollOfGame => currentRollIndex == scoreBoard.Count - 1;
        public bool IsFirstRollOfPlayer => NextPlayerToRoll == ActivePlayer;
        private Player NextPlayerToRoll => IsLastRollOfGame ? null : scoreBoard[currentRollIndex + 1].Player;
        public IList<Player> Players { get; }
        public string Winner => GetWinner();
        private bool IsGameFinished => currentRollIndex >= scoreBoard.Count;

        private string GetWinner()
        {
            if (!IsGameFinished)
                throw new GameIsNotFinishedYetException();

            return Players.OrderByDescending(GetScoreOf).First().Name;
        }

        private readonly List<Roll> scoreBoard;
        private int currentRollIndex = 0;

        public RunningGame(IList<Player> players)
        {
            scoreBoard = CreateScoreBoard(players);
            Players = players.ToImmutableList();
        }

        public void Roll(int pins)
        {
            if (IsGameFinished)
                throw new GameAlreadyFinishedException();

            var currentRoll = scoreBoard[currentRollIndex];
            currentRoll.Pins = pins;

            foreach (var pendingStrike in GetPendingStrikes())
                pendingStrike.AddBonus(pins);

            if (currentRoll.IsStrike && IsFirstRollOfPlayer)
                // STRIKE
                currentRollIndex += 2;
            else
               currentRollIndex++;
        }

        private IEnumerable<Roll> GetPendingStrikes()
        {
            return scoreBoard
                .Take(currentRollIndex)
                .Where(x => x.Player == ActivePlayer && x.IsPendingStrike);
        }

        private static List<Roll> CreateScoreBoard(IList<Player> p)
        {
            var newScoreBoard = new List<Roll>();
            for (int i = 0; i < 10; i++)
            {
                foreach (var player in p)
                {
                    newScoreBoard.Add(new Roll(player));
                    newScoreBoard.Add(new Roll(player));
                }
            }
            return newScoreBoard;
        }

        public int GetScoreOf(Player player)
        {
            return scoreBoard.Where(r => r.Player == player && r.Pins.HasValue).Sum(r => r.Pins.Value);
        }

        public int GetScoreOf(string name)
        {
            return GetScoreOf(Players.Single(p => p.Name == name));
        }

    }
}