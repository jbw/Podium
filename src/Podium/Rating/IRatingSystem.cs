using System.Collections.Generic;

namespace Podium
{
    public interface IRatingSystem
    {
        public double CalculateNewRating(IPlayer playerRating, IRating opponentRating, double result);
        public double CalculateNewRating(GameSet gameSet);
    }

}
