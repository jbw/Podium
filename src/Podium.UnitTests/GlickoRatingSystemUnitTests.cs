using System.Linq;
using Xunit;
using Shouldly;
using System.Collections.Generic;

namespace Podium.UnitTests
{
    public class GlickoRatingSystemUnitTests
    {
        [Fact]
        public void Get_New_Rating()
        {
            // Given
            IRatingSystem ratingSystem = new GlickoRatingSystem();
            IPlayer player = new PlayerRating();
            player.CurrentRating = 1500;
            player.OriginalRatingDeviation = 60;
            player.TimeSinceLastPlayed = 1;

            IRating opponent = new OpponentRating();
            opponent.CurrentRating = 1780;
            opponent.OriginalRatingDeviation = 60;

            // When
            var newRating = ratingSystem.CalculateNewRating(player, opponent, GameOutcome.Win);

            // Then
            double expectedNewRD = 1524.7282820691591;
            newRating.ShouldBe(expectedNewRD, 0.01);
        }

        [Fact]

        public void Get_New_Rating_For_A_Given_GameSet()
        {
            // Given
            IRatingSystem ratingSystem = new GlickoRatingSystem();
            IPlayer player = new PlayerRating
            {
                CurrentRating = 1500,
                OriginalRatingDeviation = 60,
                TimeSinceLastPlayed = 1
            };

            IRating opponent = new OpponentRating
            {
                CurrentRating = 1780,
                OriginalRatingDeviation = 60
            };

            var gameSet = new GameSet
            {
                Player = player,
                OpponentResults = new List<OpponentResult>
                {
                    new OpponentResult
                    {
                        Opponent = opponent,
                        Outcome = 1
                    }
                }
            };

            // When
            var newRating = ratingSystem.CalculateNewRating(gameSet);

            // Then
            double expectedNewRD = 1524.7282820691591;
            newRating.ShouldBe(expectedNewRD, 0.01);
        }
    }
}
