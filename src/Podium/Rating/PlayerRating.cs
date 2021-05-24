using System;

namespace Podium
{
    public class PlayerRating : IPlayer
    {
        public double CurrentRating { get; set; }
        public double OriginalRatingDeviation { get; set; }
        public int TimeSinceLastPlayed { get; set; }
    }

}
