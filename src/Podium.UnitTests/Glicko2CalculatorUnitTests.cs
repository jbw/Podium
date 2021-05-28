using System;
using Xunit;
using Shouldly;
using System.Collections.Generic;
using Podium.RatingSystem.Glicko2;

namespace Podium.UnitTests
{

    public class Glicko2CalculatorUnitTests
    {


        [Fact]
        public void Calculates_mu()
        {
            // Given
            double phi = 0.466273481;

            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var mu = glickoCalculator.CalculateMu(phi);

            // Then
            double expectedMu = -1.640591879;
            mu.ShouldBe(expectedMu, 0.01);
        }
    }
}
