﻿using Xunit;
using Shouldly;

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
            // When
            var newRating = ratingSystem.CalculateNewRating(player, opponent, GameOutcome.Win);


            // Then
            double expectedNewRD = 1525.0577450128465d;
            newRating.ShouldBe(expectedNewRD, 0.01);
        }
    }
}
