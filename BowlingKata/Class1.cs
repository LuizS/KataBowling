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


        [Test]
        public void Score_Game_With_Nines_And_Miss_Only()
        {
            Game game = new Game();

            for (int i = 1; i <= 10; i++)
            {
                game.Roll(9);
                game.Roll(0);
            }

            Assert.That(game.Score, Is.EqualTo(90));
        }

        [Test]
        public void Score_TwoStrikes_AndRestOne()
        {
            Game game = new Game();
            game.Roll(10);
            game.Roll(10);
            for (int i = 1; i <= 16; i++)
                game.Roll(1);

            Assert.That(game.Score, Is.EqualTo(49));
        }

        [Test]
        public void Score_PerfectGame_ResultIs300()
        {
            Game game = new Game();
            for (int i = 1; i <= 12; i++)
                game.Roll(10);
            Assert.That(game.Frames.Count,Is.EqualTo(10));
            Assert.That(game.Score, Is.EqualTo(300));
        }
    }


    public class Game
    {
        public Game()
        {
            Frames = new List<Frame>();
            BonusFrames = new List<BonusFrame>();
        }

        public List<Frame> Frames{ get; set; }
        private List<BonusFrame> BonusFrames { get; set; }

        public void Roll(int i)
        {
            if (Frames.Count < 10 || Frames.Last().FrameIsClosed == false)
            {
                if (Frames.Count() == 0 || Frames.Last().FrameIsClosed)
                {
                    Frames.Add(new Frame(i));
                }
                else
                {
                    Frames.Last().AddSecondRoll(i);
                }

            }

            AddBonus(i);

            if (!BonusFrames.Exists(b => b.Frame == Frames.Last()))
            {
                if (Frames.Last().IsStrike)
                {
                    BonusFrames.Add(BonusFrame.AddStrike(Frames.Last()));
                }
                if (Frames.Last().IsSpare)
                {
                    BonusFrames.Add(BonusFrame.AddSpare(Frames.Last()));
                }

            }

        }

        public void AddBonus(int i)
        {
            this.BonusFrames.ForEach(b => b.ComputeBonus(i));
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

        public void ComputeBonus(int i)
        {
            if (BonusRolls > 0)
            {
                this.Frame.AddBonus(i);
                BonusRolls --;
            }
            
        }
    }

    public class Frame
    {
        private int firstRoll;
        private int? secondRoll;
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

        public bool FrameIsClosed
        {
            get { return  IsStrike || secondRoll.HasValue; }
        }

        public bool IsSpare {get { return this.firstRoll + this.secondRoll == 10 && this.secondRoll > 0; }
        }

        public bool IsStrike { get { return this.firstRoll == 10; } }
        public int FrameScore { get { return firstRoll + (secondRoll.HasValue ? secondRoll.Value : 0) + bonus; } }
    }
}