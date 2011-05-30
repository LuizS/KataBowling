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

            for (int i = 1; i <= 20; i++)
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
        public void Score_Game_With_Spares_Only()
        {
            Game game = new Game();

            for (int i = 1; i <= 21; i++)
                game.Roll(5);

            Assert.That(game.Score, Is.EqualTo(150));
        }
    }

    public class Game
    {
        public void Roll(int i)
        {
            Score += i;
        }

        public int Score { get; private set; }
    }
}