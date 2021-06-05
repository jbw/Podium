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
            double opponentRating = 1215;
            double factor = 173.7177928;
            double offset = 1500;

            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var mu = glickoCalculator.CalculateMu(opponentRating, offset, factor);

            // Then
            double expectedMu = -1.64059;
            mu.ShouldBe(expectedMu, 0.01);
        }

        [Fact]
        public void Calculates_phi()
        {
            // Given
            double opponentRD = 81;
            double factor = 173.7177928;

            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var phi = glickoCalculator.CalculatePhi(opponentRD, factor);

            // Then
            double expectedPhi = 0.466273481;
            phi.ShouldBe(expectedPhi, 0.01);
        }

        [Fact]
        public void Calculates_g()
        {
            // Given
            double phi = 0.466273481;

            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var g = glickoCalculator.CalculateG(phi);

            // Then
            double expectedG = 0.96850994;
            g.ShouldBe(expectedG, 0.01);
        }


        [Fact]
        public void Calculates_E()
        {
            // Given
            double playerMu = -4.0123;
            double g = 0.9685;
            double opponentMu = -1.6405;

            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var E = glickoCalculator.CalculateE(g, opponentMu, playerMu);

            // Then
            double expectedE = 0.091373481;
            E.ShouldBe(expectedE, 0.01);
        }

        [Fact]
        public void Calculates_G2E()
        {
            // Given
            double E = 0.091373481;
            double g = 0.9685;
            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var G2E = glickoCalculator.CalculateG2E(g, E);

            // Then
            double expectedG2E = 0.077877;
            G2E.ShouldBe(expectedG2E, 0.01);
        }


        [Fact]
        public void Calculates_GsE()
        {
            // Given
            double E = 0.091373481;
            double g = 0.9685;
            double outcome = GameOutcome.Win;
            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var GsE = glickoCalculator.CalculateGsE(g, E, outcome);

            // Then
            double expectedGsE = 0.8800138;
            GsE.ShouldBe(expectedGsE, 0.01);
        }

        [Fact]
        public void Calculates_Nu()
        {
            // Given
            double g2ESum = 0.077877;
            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var nu = glickoCalculator.CalculateNu(g2ESum);

            // Then
            double expectedNu = 12.84062;
            nu.ShouldBe(expectedNu, 0.01);
        }

        [Fact]
        public void Calculates_Delta()
        {
            // Given
            double GsESum = 0.8800138;
            double nu = 12.84062;
            IGlicko2RatingCalculator glickoCalculator = new Glicko2RatingCalculator();

            // When
            var delta = glickoCalculator.CalculateDelta(GsESum, nu);

            // Then
            double expectedDelta = 11.2999;
            delta.ShouldBe(expectedDelta, 0.01);
        }
    }
}
