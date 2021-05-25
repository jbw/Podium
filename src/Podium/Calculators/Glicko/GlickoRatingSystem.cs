using System;
using System.Collections.Generic;

namespace Podium
{
    public class GlickoRatingSystem : IRatingSystem
    {
        private readonly IGlickoRatingCalculator _glickoRatingCalculator;
        private readonly double Q = Math.Log(10) / 400;
        private readonly int SkillUncertainty = 1800;

        public GlickoRatingSystem()
        {
            _glickoRatingCalculator = new GlickoRatingCalculator();
        }

        public double CalculateNewRating(GameSet gameSet)
        {
            return _glickoRatingCalculator.CalculateNewRating(gameSet, Q, SkillUncertainty);
        }

        public double CalculateNewRating(IPlayer playerRating, IRating opponentRating, double result)
        {
            return _glickoRatingCalculator.CalculateNewRating(
                result,
                playerRating.CurrentRating,
                opponentRating.CurrentRating,
                playerRating.OriginalRatingDeviation,
                opponentRating.OriginalRatingDeviation,
                Q,
                SkillUncertainty
            );
        }
    }
}
