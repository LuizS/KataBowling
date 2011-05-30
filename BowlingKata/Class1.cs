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
        private List<BonusFrame> BonusFrames { get; set; }

        public void Roll(int i)
        {
            if (openFrame == null)
                openFrame = new Frame(i);
            else
                openFrame.AddSecondRoll(i);
            if (openFrame.IsStrike)
            {
                BonusFrames.Add(BonusFrame.AddStrike(openFrame));
            }
            if (openFrame.IsSpare)
            {
                BonusFrames.Add(BonusFrame.AddSpare(openFrame));
            }
        }

        public int Score
        {
            get { return this.Frames.Sum(f => f.FrameScore); }
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

    internal class BonusFrame
    {
        private int BonusRolls;

        public Frame Frame
    {
        get;
        set;
    }

        public static BonusFrame AddStrike(Frame openFrame)
        {
            return new BonusFrame {BonusRolls = 2, Frame = openFrame};
        }

        public static BonusFrame AddSpare(Frame openFrame)
        {
            return new BonusFrame {BonusRolls = 1, Frame = openFrame};
        }
    }

    public class Frame
    {
        private int firstRoll, secondRoll;
        private int bonus;

        public Frame(int firstRoll)
        {
            this.firstRoll = firstRoll;
        }

        public void AddSecondRoll(int secondRoll)
        {
            this.secondRoll = secondRoll;
        }

        public void AddBonus(int bonus)
        {
            this.bonus += bonus;
        }

        public bool IsSpare {get { return this.firstRoll + this.secondRoll == 10 && this.secondRoll > 0; }
        }

        public bool IsStrike { get { return this.firstRoll == 10; } }
        public int FrameScore { get { return firstRoll + secondRoll + bonus; } }
    }
}