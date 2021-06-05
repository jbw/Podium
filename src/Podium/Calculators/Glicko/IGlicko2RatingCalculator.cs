namespace Podium
{
    public interface IGlicko2RatingCalculator
    {
        public double CalculateMu(double opponentRating, double offset, double factor);
        public double CalculatePhi(double opponentRD, double factor);
        public double CalculateG(double phi);
        public double CalculateE(double g, double opponentMu, double playerMu);
        public double CalculateG2E(double phi, double e);
        public double CalculateGsE(double phi, double e, double outcome);
        public double CalculateNu(double g2ESum);
        public double CalculateDelta(double gsESum, double nu);
    }

}
