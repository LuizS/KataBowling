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
        public Game()
        {
            Frames = new List<Frame>();
        }

        public List<Frame> Frames{ get; set; }
        private Frame openFrame;

        public void Roll(int i)
        {
            if (openFrame == null)
                openFrame = new Frame(i);
            else
                openFrame.AddSecondRoll(i);
        }

        public int Score
        {
            get { return CalculateScore(); }
        }

        private int CalculateScore()
        {
            int sum = 0;
            foreach (var frame in Frames)
            {
                sum += frame.FrameScore;
            }

            return sum;
        }
    }

    public class Frame
    {
        private int firstRoll, secondRoll;

        public Frame(int firstRoll)
        {
            this.firstRoll = firstRoll;
        }

        public void AddSecondRoll(int secondRoll)
        {
            this.secondRoll = secondRoll;
        }

        public int FrameScore { get { return firstRoll + secondRoll; } }
    }
}