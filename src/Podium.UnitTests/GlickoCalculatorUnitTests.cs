using System;
using Xunit;
using Shouldly;
using System.Collections.Generic;

namespace Podium.UnitTests
{

    public class GlickoCalculatorUnitTests
    {

        [Fact]
        public void Calculates_Onset_Rating_Deviation()
        {
            // Given
            float ratingDeviation = 60;
            float timeSinceLastRating = 1;
            float skillUncertainity = 1800;

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var RD = glickoCalculator.CalculateOnSetDeviation(ratingDeviation, timeSinceLastRating, skillUncertainity);

            // Then
            double expectedRD = 73.48469228349535;

            RD.ShouldBe(expectedRD, 0.01);
        }


        [Fact]
        public void Calculates_Weighting_Gravity_Value()
        {
            // Given
            float ratingDeviation = 60;
            double q = Math.Log(10) / 400;

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var weight = glickoCalculator.CalculateWeighting(ratingDeviation, q);

            // Then
            double expectedWeight = 0.9823483039494204;

            weight.ShouldBe(expectedWeight, 0.01);
        }

        [Fact]
        public void Calculates_Expected_Fractional_Score()
        {
            // Given
            double playerRating = 1500;
            double opponentRating = 1780;
            double weighting = 0.973841409;

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var score = glickoCalculator.CalculateExpectedFractionScore(playerRating, opponentRating, weighting);

            // Then
            double expectedScore = 0.17226673505438217;
            score.ShouldBe(expectedScore, 0.01);
        }

        [Fact]
        public void Calculates_New_Rating_Deviation()
        {
            // Given
            double RD = 73.48469228349535;
            double q = Math.Log(10) / 400;
            double weighting = 0.973841409;
            double fractionalScore = 0.17226673505438217;

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var newRD = glickoCalculator.CalculateNewRatingDeviation(RD, weighting, q, fractionalScore);

            // Then
            double expectedNewRD = 72.61142990453034;
            newRD.ShouldBe(expectedNewRD, 0.01);
        }

        [Fact]
        public void Calculates_New_Rating()
        {
            // Given
            double playerRating = 1500;
            double opponentRating = 1780;
            double playerDeviation = 60;
            double opponentDeviation = 60;
            double q = Math.Log(10) / 400;
            double skillUncertainty = 1800;
            double result = GameOutcome.Win;

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var rating = glickoCalculator.CalculateNewRating(
                result, 
                playerRating, 
                opponentRating, 
                playerDeviation, 
                opponentDeviation, 
                q, 
                skillUncertainty
            );

            // Then
            double expectedNewRD = 1524.738;
            rating.ShouldBe(expectedNewRD, 0.01);
        }


        [Fact]
        public void Calculates_New_Rating_For_More_Than_One_Game_Result()
        {
            // Given
            double q = Math.Log(10) / 400;
            float skillUncertainity = 1800;

            IPlayer player = new PlayerRating
            {
                CurrentRating = 1500,
                OriginalRatingDeviation = 60,
                TimeSinceLastPlayed = 1
            };

            IRating opponent1 = new OpponentRating
            {
                CurrentRating = 1780,
                OriginalRatingDeviation = 60
            };

            IRating opponent2 = new OpponentRating
            {
                CurrentRating = 1880,
                OriginalRatingDeviation = 60
            };

            IRating opponent3 = new OpponentRating
            {
                CurrentRating = 1980,
                OriginalRatingDeviation = 60
            };

            var gameSet = new GameSet
            {
                Player = player,
                OpponentResults = new List<OpponentResult>
                {
                    new OpponentResult
                    {
                        Outcome = GameOutcome.Win,
                        Opponent = opponent1
                    },
                    new OpponentResult
                    {
                        Outcome = GameOutcome.Win,
                        Opponent = opponent2
                    },
                    new OpponentResult
                    {
                        Outcome = GameOutcome.Win,
                        Opponent = opponent3
                    }
                }
            };

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var rating = glickoCalculator.CalculateNewRating(gameSet, q, skillUncertainity);

            // Then
            double expectedNewRD = 1577.2469;
            rating.ShouldBe(expectedNewRD, 0.01);
        }

        [Fact]
        public void Calculates_New_Rating_For_More_One_Game_Result()
        {
            // The g (S - E) factor is totalled for each game and a single rpost calculation is performed.
            // Given
            double q = Math.Log(10) / 400;
            float skillUncertainity = 1800;

            IPlayer player = new PlayerRating
            {
                CurrentRating = 1500,
                OriginalRatingDeviation = 60,
                TimeSinceLastPlayed = 1
            };

            IRating opponent = new OpponentRating
            {
                CurrentRating = 1780,
                OriginalRatingDeviation = 60,

            };

            var results = new GameSet
            {
                Player = player,
                OpponentResults = new List<OpponentResult>
                {
                    new OpponentResult
                    {
                        Outcome = GameOutcome.Win,
                        Opponent = opponent
                    }
                }
            };

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var rating = glickoCalculator.CalculateNewRating(results, q, skillUncertainity);

            // Then
            double expectedNewRD = 1524.7282820691591;
            rating.ShouldBe(expectedNewRD, 0.01);
        }
    }
}
