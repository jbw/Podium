using System.Collections.Generic;

namespace Podium
{
    public interface IGlickoRatingCalculator
    {
        public double CalculateNewRating(double gameOutcome, double playerRating, double opponentRating, double ratingDeviation, double opponentRatingDeviation, double q, double skillUncertainty);
        public double CalculateNewRating(GameSet gameSet, double q, double skillUncertainty);
        public double CalculateWeighting(double RD, double q);
        public double CalculateOnSetDeviation(double oldRatingDeviation, double timeSinceLastRating, double skillUncertainity);
        public double CalculateNewRatingDeviation(double onsetRatingDeviation, double q, double weight, double fractionalScore);
        public double CalculateExpectedFractionScore(double playerRating, double opponentRating, double weighting);
    }

}
