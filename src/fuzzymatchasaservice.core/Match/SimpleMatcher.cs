using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace fuzzymatchasaservice.core.Match
{
    public class SimpleMatcher<T> : IMatcher<T>
    {
        public IDictionary<string, MatchRule> Rules { get; set; }
        public T Target { get; set; }

        private SortedList<string, object> _targetValues;

        protected SortedList<string, object> TargetValues
        {
            get
            {
                return _targetValues ?? (_targetValues = new SortedList<string, object>(
                    Rules.Select(rule => GetPropertyValue(rule.Key, Target)).Where(x => x != null).ToDictionary(k => k.Item1, v => v.Item2)));
            }
        } 

        public float Score(T o)
        {
            var subjectValues =
                new SortedList<string, object>(
                    Rules.Select(rule => GetPropertyValue(rule.Key, o)).Where(x => x != null).ToDictionary(k => k.Item1, v => v.Item2));

            return subjectValues.Zip(_targetValues, (subjectPair, targetPair) =>
            {
                var rule = Rules[targetPair.Key];
                var score = rule.Scorer(subjectPair.Value, targetPair.Value)*rule.Weight;
                return score;
            }).Aggregate((x, y) => x + y);
        }

        private static Tuple<string, object> GetPropertyValue(string key, T subject)
        {
            var propInfo = typeof(T).GetRuntimeProperty(key);
            if (propInfo != null)
            {
                return new Tuple<string, object>(key, propInfo.GetValue(subject));
            }

            return null;
        }
    }
}