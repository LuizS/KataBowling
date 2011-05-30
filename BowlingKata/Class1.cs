using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BowlingKata
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Score_UserGetsNoPoint_ReturnsZero()
        {
            Game game = new Game();

            for (int i = 1; i <= 20; i++ )
                game.Roll(0);

            Assert.That(game.Score, Is.EqualTo(0));
        }
    }

    public class Game
    {
        public void Roll(int i)
        {
            throw new NotImplementedException();
        }

        public int Score { get; private set; }
    }
}
