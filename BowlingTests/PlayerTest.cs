using System;
using System.Linq;
using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        [DataRow(5)]
        public void TestFirstRoll(int knockedPin)
        {
            var player = new Player();
            player.Roll(knockedPin);

            Assert.AreEqual(knockedPin, player.Score);
        }
    }
}
