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

        [Test]
        public void Score_UserGetsPinsWithOnlyOnePin_ReturnsTwenty()
        {
            Game game = new Game();

            for (int i = 1; i <= 20; i++)
                game.Roll(1);

            Assert.That(game.Score, Is.EqualTo(20));
        }

        [Test]
        public void Score_UserGetsPinsWithNinePins_Returns180()
        {
            Game game = new Game();

            for (int i = 1; i <= 20; i++)
                game.Roll(9);

            Assert.That(game.Score, Is.EqualTo(180));
        }
    }

    public class Game
    {
        public void Roll(int i)
        {
            
        }

        public int Score { get; private set; }
    }
}
