namespace Podium
{
    public interface IRating
    {
        public double CurrentRating { get; set; }
        public double OriginalRatingDeviation { get; set; }
    }

    public interface IPlayer : IRating
    {
        /// <summary>
        /// t
        /// </summary>
        public int TimeSinceLastPlayed { get; set; }
    }

}
