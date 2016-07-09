using System;
using System.Collections.Generic;
using System.Linq;

namespace fuzzymatchasaservice.core.Match
{
    public class MatchRunner<T>
    {
        private readonly DataSource<T> _dataSource;
        private readonly IMatcher<T> _matcher;

        public MatchRunner(DataSource<T> dataSource, IMatcher<T> matcher)
        {
            _dataSource = dataSource;
            _matcher = matcher;
        }

        public IDictionary<float, T> Results(T target, int top, Func<T, bool> predicate)
        {
            _matcher.Target = target;
            var heap = new SortedDictionary<float, T>();

            foreach (var @object in _dataSource.GetItems(predicate))
            {
                var score = _matcher.Score(@object);
                if (heap.Count >= top)
                {
                    var minKey = heap.Keys.First();
                    if (!(minKey < score)) continue;
                    heap.Remove(minKey);
                    heap.Add(score, @object);
                }
                else
                {
                    heap.Add(score, @object);
                }
            }

            return heap;

        }
    }
}