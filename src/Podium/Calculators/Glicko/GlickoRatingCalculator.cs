using System;
using System.Collections.Generic;

namespace Podium
{
    public class GlickoRatingCalculator : IGlickoRatingCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameSet"></param>
        /// <param name="q"></param>
        /// <param name="skillUncertainty">c</param>
        /// <returns></returns>
        public double CalculateNewRating(GameSet gameSet, double q, double skillUncertainty)
        {

            double newOnSetRD = Math.Sqrt(Math.Pow(gameSet.Player.OriginalRatingDeviation, 2) + skillUncertainty);

            var (weight, factor) = CalculateOpponentWeightingAndFactor(gameSet, q, skillUncertainty);

            double d2 = CalculateD2(q, weight);
            double newRd = CalculateNewRatingDeviation(newOnSetRD, d2);

            var newRating = gameSet.Player.CurrentRating + q * Math.Pow(Math.Round(newRd, 0), 2) * factor;
            return newRating;
        }

        /// <summary>
        /// Onset RD
        /// </summary>
        /// <param name="ratingDeviation"></param>
        /// <param name="timeSinceLastRating"></param>
        /// <param name="skillUncertainity"></param>
        /// <returns></returns>
        public double CalculateOnSetDeviation(double ratingDeviation, double timeSinceLastRating, double skillUncertainity)
        {
            var onSetDeviation = (Math.Pow(ratingDeviation, 2) + (skillUncertainity * timeSinceLastRating));
            onSetDeviation = Math.Sqrt(onSetDeviation);

            return onSetDeviation;
        }

        /// <summary>
        /// g
        /// </summary>
        /// <param name="RD"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public double CalculateWeighting(double RD, double q)
        {
            var g = 1 / Math.Sqrt(1 + (3 * Math.Pow(q, 2)) * Math.Pow(RD, 2) / Math.Pow(Math.PI, 2));

            return g;
        }

        /// <summary>
        /// E
        /// </summary>
        /// <param name="playerRating"></param>
        /// <param name="opponentRating"></param>
        /// <param name="weighting"></param>
        /// <returns></returns>
        public double CalculateExpectedFractionScore(double playerRating, double opponentRating, double weighting)
        {
            var score = -weighting * (playerRating - opponentRating) / 400;
            score = 1 / (1 + Math.Pow(10, score));

            return score;
        }

        /// <summary>
        /// D Squared
        /// </summary>
        /// <param name="q"></param>
        /// <param name="weight"></param>
        /// <param name="fractionalScore"></param>
        /// <returns></returns>
        public double CalculateD2(double q, double weight, double fractionalScore)
        {
            var d2 = 1 / (Math.Pow(q, 2) * Math.Pow(weight, 2) * fractionalScore * (1 - fractionalScore));
            return d2;
        }

        /// <summary>
        /// D Sqaured
        /// </summary>
        /// <param name="q"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public double CalculateD2(double q, double weight)
        {
            var d2 = 1 / (Math.Pow(q, 2) * weight);
            return d2;
        }

        /// <summary>
        /// RD new
        /// </summary>
        /// <param name="ratingDeviation"></param>
        /// <param name="q"></param>
        /// <param name="weight"></param>
        /// <param name="fractionalScore"></param>
        /// <returns></returns>
        public double CalculateNewRatingDeviation(double ratingDeviation, double q, double weight, double fractionalScore)
        {
            var d2 = CalculateD2(q, weight, fractionalScore);

            var newRD = (1 / Math.Pow(ratingDeviation, 2)) + (1 / d2);
            newRD = 1 / Math.Sqrt(newRD);

            return newRD;
        }

        public double CalculateNewRatingDeviation(double ratingDeviation, double d2)
        {
            var newRD = (1 / Math.Pow(ratingDeviation, 2)) + (1 / d2);
            newRD = 1 / Math.Sqrt(newRD);

            return newRD;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="playerRating"></param>
        /// <param name="opponentRating"></param>
        /// <param name="ratingDeviation"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public double CalculateNewRating(double result, double playerRating, double opponentRating, double ratingDeviation, double q)
        {
            var weight = CalculateWeighting(ratingDeviation, q);
            var fractionalScore = CalculateExpectedFractionScore(playerRating, opponentRating, weight);

            var newRating = playerRating + q * Math.Pow(ratingDeviation, 2) * weight * (result - fractionalScore);
            return newRating;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameSet"></param>
        /// <param name="q"></param>
        /// <param name="skillUncertainty"></param>
        /// <returns></returns>
        public (double weight, double factor) CalculateOpponentWeightingAndFactor(GameSet gameSet, double q, double skillUncertainty)
        {
            double weight = 0;
            double factor = 0;

            foreach (var result in gameSet.OpponentResults)
            {

                // Opponent onset RD
                var gameResultOnsetRD = CalculateOnSetDeviation(
                    result.Opponent.OriginalRatingDeviation,
                    gameSet.Player.TimeSinceLastPlayed,
                    skillUncertainty
                );

                // g
                var weighting = CalculateWeighting(gameResultOnsetRD, q);

                // E
                var fractionalScore = CalculateExpectedFractionScore(
                    gameSet.Player.CurrentRating,
                    result.Opponent.CurrentRating,
                    weighting
                );

                // g^2 * E(1-E)
                weight += Math.Pow(weighting, 2) * fractionalScore * (1 - fractionalScore);

                // g(S-E)
                factor += weighting * (result.Outcome - fractionalScore);
            }

            return (weight, factor);
        }
    }
}
