using System.Collections.Generic;

namespace Podium
{
    public static class GameOutcome
    {
        public const double Win = 1;
        public const double Draw = 0.5;
        public const double Lose = 0;
    }

    public class GameSet
    {
        public IPlayer Player { get; set; }

        public IList<OpponentResult> OpponentResults { get; set; }
    }

    public class OpponentResult
    {
        public IRating Opponent { get; set; }
        public double Outcome { get; set; }
    }


}
