using System;
using System.Collections.Generic;

namespace Podium
{
    public class GlickoRatingSystem : IRatingSystem
    {
        private readonly IGlickoRatingCalculator _glickoRatingCalculator;
        private readonly double Q = Math.Log(10) / 400;
        private readonly int SkillUncertainty = 1800;

        public GlickoRatingSystem(IGlickoRatingCalculator glickoRatingCalculator)
        {
            _glickoRatingCalculator = glickoRatingCalculator;
        }

        public double CalculateNewRating(IList<double> gameResults)
        {
            throw new NotImplementedException();
        }

        public double CalculateNewRating(IPlayer playerRating, IRating opponentRating, double result)
        {
            var onsetRD = _glickoRatingCalculator.CalculateOnSetDeviation(
                playerRating.OriginalRatingDeviation,
                playerRating.TimeSinceLastPlayed,
                SkillUncertainty
            );

            return _glickoRatingCalculator.CalculateNewRating(
                result,
                playerRating.CurrentRating,
                opponentRating.CurrentRating,
                onsetRD,
                Q
            );
        }
    }
}
