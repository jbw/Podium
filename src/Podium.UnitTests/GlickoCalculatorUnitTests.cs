using System;
using Xunit;
using Shouldly;

namespace Podium.UnitTests
{
    public class GlickoCalculatorUnitTests
    {
        [Fact]

        public void Get_New_Rating()
        {
            // Given
            IGlickoRatingCalculator glickoRatingCalculator = new GlickoRatingCalculator();
            IRatingSystem ratingSystem = new GlickoRatingSystem(glickoRatingCalculator);
            IPlayer player = new PlayerRating();
            player.CurrentRating = 1500;
            player.OriginalRatingDeviation = 60;
            player.TimeSinceLastPlayed = 1;


            IRating opponent = new OpponentRating();
            opponent.CurrentRating = 1780;
            // When
            var newRating = ratingSystem.CalculateNewRating(player, opponent, GameResult.Win);


            // Then
            double expectedNewRD = 1525.0577450128465d;
            newRating.ShouldBe(expectedNewRD);
        }

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

            RD.ShouldBe(expectedRD);
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

            weight.ShouldBe(expectedWeight);
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
            score.ShouldBe(expectedScore);
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
            var newRD = glickoCalculator.CalculateNewRatingDeviation(RD, q, weighting, fractionalScore);

            // Then
            double expectedNewRD = 72.61142990453034;
            newRD.ShouldBe(expectedNewRD);
        }

        [Fact]
        public void Calculates_New_Rating()
        {
            // Given
            double RD = 73.48469228349535;
            double playerRating = 1500;
            double opponentRating = 1780;
            double q = Math.Log(10) / 400;
            double result = GameResult.Win; 

            IGlickoRatingCalculator glickoCalculator = new GlickoRatingCalculator();

            // When
            var rating = glickoCalculator.CalculateNewRating(result, playerRating, opponentRating, RD, q);

            // Then
            double expectedNewRD = 1525.0577450128465d;
            rating.ShouldBe(expectedNewRD);
        }

        [Fact]
        public void Calculates_New_Rating_Deviation_For_More_Than_One_Game_Result()
        {
            // new G and E calculation for each result
            // the g2 E(1 - E) for each opponent is totalled and a single d2 
            // is calculated for the whole period.
            throw new NotImplementedException();
        }

        [Fact]
        public void Calculates_New_Rating_For_More_Than_One_Game_Result()
        {
            // The g (S - E) factor is totalled for each game and a single rpost calculation is performed.
            throw new NotImplementedException();
        }
    }
}
