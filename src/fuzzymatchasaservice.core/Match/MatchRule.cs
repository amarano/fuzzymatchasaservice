using System;

namespace fuzzymatchasaservice.core.Match
{
    public struct MatchRule
    {
        public MatchRule(Func<object, object, float> scorer, float weight)
        {
            Scorer = scorer;
            Weight = weight;
        }

        public Func<object, object, float> Scorer { get; private set; }
        public float Weight { get; private set; }
    }
}