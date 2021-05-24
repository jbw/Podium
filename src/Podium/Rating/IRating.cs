namespace Podium
{
    public interface IRating
    {
        public double CurrentRating { get; set; }

    }

    public interface IPlayer : IRating
    {

        /// <summary>
        /// RD old
        /// </summary>
        public double OriginalRatingDeviation { get; set; }

        /// <summary>
        /// t
        /// </summary>
        public int TimeSinceLastPlayed { get; set; }
    }

}
