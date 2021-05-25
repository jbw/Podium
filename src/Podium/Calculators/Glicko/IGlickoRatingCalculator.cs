using System.Collections.Generic;

namespace Podium
{
    public interface IGlickoRatingCalculator
    {
        public double CalculateNewRating(double result, double playerRating, double opponentRating, double ratingDeviation, double q);
        public double CalculateNewRating(GameSet gameSet, double q, double skillUncertainty);
        public double CalculateWeighting(double RD, double q);
        public double CalculateOnSetDeviation(double oldRatingDeviation, double timeSinceLastRating, double skillUncertainity);
        public double CalculateNewRatingDeviation(double onsetRatingDeviation, double q, double weight, double fractionalScore);
        public double CalculateExpectedFractionScore(double playerRating, double opponentRating, double weighting);
    }

}
