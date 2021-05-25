using System;

namespace Podium
{
    public class OpponentRating : IRating
    {
        public double CurrentRating { get; set; }
        public double OriginalRatingDeviation { get; set; }
    }
}
