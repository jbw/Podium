using System;
using System.Collections.Generic;

namespace Podium.RatingSystem.Glicko2
{
    public class Glicko2RatingCalculator : IGlicko2RatingCalculator
    {
        public double CalculateE(double phi)
        {
            throw new NotImplementedException();
        }

        public double CalculatePhi(double opponentRD, double factor)
        {
            return opponentRD / factor;
        }

        public double CalculateG(double phi)
        {
            var g = 1 + 3 * Math.Pow(phi, 2) * Math.Pow(Math.PI, -2);

            g = Math.Pow(g, -0.5);

            return g;
        }

        public double CalculateG2E(double phi)
        {
            throw new NotImplementedException();
        }

        public double CalculateMu(double opponentRating, double offset, double factor)
        {
            var mu = (opponentRating - offset) / factor;

            return mu;
        }

        public double CalculateE(double g, double opponentMu, double playerMu)
        {
            double E = 1 + Math.Exp(g * (opponentMu - playerMu));
            E = Math.Pow(E, -1);

            return E;
        }

        public double CalculateG2E(double g, double E)
        {
            var G2E = Math.Pow(g, 2) * E * (1 - E);

            return G2E;
        }

        public double CalculateGsE(double g, double E, double outcome)
        {
            double GsE = g * (outcome - E);

            return GsE;
        }
    }
}